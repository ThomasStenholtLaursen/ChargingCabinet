using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Library
{
    public class ChargeControl : IChargeControl
    {
        public double Current { get; set; }
        public bool Connected { get; set; }
        
        private readonly IUsbCharger _usbCharger;
        private readonly IDisplay _display;

        public ChargeControl(IUsbCharger usbCharger, IDisplay display)
        {
            usbCharger.CurrentValueEvent += HandleNewCurrent;
            _usbCharger = usbCharger;
            _display = display;
        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }

        public bool IsConnected()
        {
            return _usbCharger.Connected;
        }

        public void HandleNewCurrent(object sender, CurrentEventArgs e)
        {
            Current = e.Current;

            if (Current == 0)
            {
                _display.Print("");
            }
            else if (Current > 0 && Current <= 5)
            {
                _display.Print("Device is fully charged!");
                StopCharge();
            }
            else if (Current > 5 && Current <= 500)
            {
                _display.Print("Device is charging!");
            }
            else
            {
                _display.Print("Error!");
                StopCharge();
            }
        }
    }
}
