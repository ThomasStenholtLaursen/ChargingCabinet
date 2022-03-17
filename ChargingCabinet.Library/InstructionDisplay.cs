using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Library
{
	public class InstructionDisplay : IDisplay
	{
		public string printText { get; set; }

		public void Print(string printString)
		{
			printText = printString;
			Console.WriteLine(printText);
		}
	}
}
