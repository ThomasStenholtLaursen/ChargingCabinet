using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Library
{
	public class InstructionDisplay : IDisplay
	{
		public string PrintText { get; set; }

		public void Print(string printString)
		{
			PrintText = printString;
			Console.WriteLine(PrintText);
		}
	}
}
