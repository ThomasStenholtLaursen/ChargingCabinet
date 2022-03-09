using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Library
{
    public class ChargeEventArgs : EventArgs
    {
        public double Connected { set; get; }
    }
    public interface IChargeControl
    {
        event EventHandler<ChargeEventArgs> StatusChangedEvent;
    }
}
