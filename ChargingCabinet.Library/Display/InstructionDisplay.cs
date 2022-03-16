using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Library
{
	class InstructionDisplay : IDisplay
	{
		public void Print(string printString)
		{
			Console.WriteLine(printString);
		}
	}
}
