using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Library
{
   public interface IDisplay
    {
	    string PrintText { get; set; }
	    void Print(string printString);
    }
}
