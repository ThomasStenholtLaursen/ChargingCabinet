using System;
using ChargingCabinet.Library;
using ChargingCabinet.Library.Logging;

namespace ChargingCabinet.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            bool finish = false;
            Door door = new Door();
            RFIDReader rfidReader = new RFIDReader();
            IDisplay chDisp = new ChargeDisplay();
            IDisplay insDisplay = new InstructionDisplay();
            IUsbCharger usbCharger = new UsbChargerSimulator();
            IChargeControl chargeControl = new ChargeControl(usbCharger, chDisp);
            ILog log = new FileLog();
            StationControl stationControl = new StationControl(door,insDisplay,chDisp,rfidReader,chargeControl, log);

            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, T, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E': //Stopper terminal
                        finish = true;
                        break;

                    case 'O': //Bruger åbner dør
                        door.OnDoorOpened(); 
                        break;
                    case 'T': //Bruger tilslutter telefon
                        usbCharger.Connected = true;
                        Console.WriteLine("'Bruger tilslutter telefon'"); 
                        break;

                    case 'C': //Bruger lukker dør
                        door.OnDoorClosed(); 
                        break;

                    case 'R': //Bruger indlæser med RFID
                        System.Console.WriteLine("Indtast RFID id: ");
                        string idString = System.Console.ReadLine();

                        int id = Convert.ToInt32(idString);
                        rfidReader.SetRFID(id);
                        break;

                    default:
                        break;
                }

            } while (!finish);
        }
    }
}
