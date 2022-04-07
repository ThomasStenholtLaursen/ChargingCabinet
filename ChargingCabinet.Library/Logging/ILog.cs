using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Library.Logging
{
    public interface ILog
    {
        void Log(string logFile, string logString, int id);
    }
}
