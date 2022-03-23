using System;
using ChargingCabinet.Library;

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
            StationControl stationControl = new StationControl(door,insDisplay,chDisp,rfidReader,chargeControl);

            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

                switch (input[0])
                {
                    case 'E':
                        finish = true;
                        break;

                    case 'O':
                        door.OnDoorOpened();
                        break;

                    case 'C':
                        door.OnDoorClosed();
                        break;

                    case 'R':
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
