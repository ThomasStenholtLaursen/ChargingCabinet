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
        public string DisplayText { get; set; }
        public bool Connected { get; set; }
        
        private readonly IUsbCharger _usbCharger;

        public ChargeControl(IUsbCharger usbCharger)
        {
            usbCharger.CurrentValueEvent += HandleNewCurrent;
            _usbCharger = usbCharger;
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
            return Connected;
        }

        public void HandleNewCurrent(object sender, CurrentEventArgs e)
        {
            Current = e.Current;

            if (Current == 0)
            {
                DisplayText = "";
                Connected = false;
            }
            else if (Current > 0 && Current <= 5)
            {
                DisplayText = "Device is fully charged!";
                Connected = true;
                StopCharge();
            }
            else if (Current > 5 && Current <= 500)
            {
                DisplayText = "Device is charging!";
                Connected = true;
            }
            else
            {
                DisplayText = "Error!";
                StopCharge();
            }
        }
    }
}
