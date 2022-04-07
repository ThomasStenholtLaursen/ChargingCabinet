using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Library.Logging
{
    public class FileLog : ILog
    {
        public void Log(string logFile, string logString, int id)
        {
            using (var writer = File.AppendText(logFile))
            {
                writer.WriteLine(DateTime.Now + logString, id);
            }
        }
    }
}
