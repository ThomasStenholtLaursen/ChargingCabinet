﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Library
{
    interface IDisplay
    {
	    string printText { get; set; }
	    void Print(string printString);
    }
}
