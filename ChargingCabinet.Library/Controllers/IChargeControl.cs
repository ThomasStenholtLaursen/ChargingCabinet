using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Library
{
    
    public interface IChargeControl
    {
        public double Current { get; set; }
        public string DisplayText { get; set; }
        public bool Connected { get; set; }
        void StartCharge();
        void StopCharge();
        bool IsConnected();
        void HandleNewCurrent(object sender, CurrentEventArgs e);
    }
}
