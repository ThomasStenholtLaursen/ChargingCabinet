using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Library
{
   public class StationControl
   {
      // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
      public enum ChargingCabinet
      {
         Available,
         Locked,
         DoorOpen
      };

      // Her mangler flere member variable
      public ChargingCabinet _state { get; set; }
      private IChargeControl _charger;
      public int _oldId { get; private set; }
      public int _newId { private set; get; }
      public bool Connected { get; set; }
      private IDoor _door;
      private IDisplay _instructionsDisplay;
      private IDisplay _chargeDisplay;
      private IRFIDReader _rfidReader;
      public bool State { get; set; }

      private string logFile = "logfile.txt"; // Navnet på systemets log-fil

      // Her mangler constructor
      public StationControl(IDoor door, IDisplay instructionDisplay, IDisplay chargeDisplay, IRFIDReader rfidReader, IChargeControl chargeControl)
      {
         _door = door;
         _instructionsDisplay = instructionDisplay;
         _chargeDisplay = chargeDisplay;
         _rfidReader = rfidReader;
         _charger = chargeControl;
         _door.DoorOpenedEvent += DoorOpened;
         _door.DoorClosedEvent += DoorClosed;
         _rfidReader.RFIDDetectedEvent += OnNewRfid;
      }

      // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
      public void RfidDetected(int id)
      {
         switch (_state)
         {
            case ChargingCabinet.Available:
               // Check for ladeforbindelse
               Connected = _charger.IsConnected();
               if (Connected)
               {
                  _door.LockDoor();
                  _charger.StartCharge();
                  _oldId = id;
                  using (var writer = File.AppendText(logFile))
                  {
                     writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                  }

                  _instructionsDisplay.Print("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                  _state = ChargingCabinet.Locked;
               }
               else
               {
                  _instructionsDisplay.Print("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
               }

               break;

            case ChargingCabinet.DoorOpen:
               // Ignore
               break;

            case ChargingCabinet.Locked:
               // Check for correct ID
               if (id == _oldId)
               {
                  _charger.StopCharge();
                  _door.UnlockDoor();
                  using (var writer = File.AppendText(logFile))
                  {
                     writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                  }

                  _instructionsDisplay.Print("Tag din telefon ud af skabet og luk døren");
                  _state = ChargingCabinet.Available;
               }
               else
               {
                  _instructionsDisplay.Print("Forkert RFID tag");
               }

               break;
         }
      }

      public void DoorOpened(object sender, DoorOpenedEventArgs e)
      {
         _state = ChargingCabinet.DoorOpen;
         State = e.State;
         _instructionsDisplay.Print("Tilslut telefon");
      }

      public void DoorClosed(object sender, DoorClosedEventArgs e)
      {
         State = e.State;
         _instructionsDisplay.Print("Indlæs RFID");
      }

      public void OnNewRfid(object sender, RFIDEventArgs e)
      {
         _newId = Convert.ToInt32(e.Detected);
         RfidDetected(_newId);
      }


   }
}
