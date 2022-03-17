using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingCabinet.Library;
using NUnit.Framework;

namespace ChargingCabinet.Test
{
	class DisplayUnitTest
	{
		private ChargeDisplay uut1;
		private InstructionDisplay uut2;
		
		[SetUp]
		public void Setup()
		{
			uut1 = new ChargeDisplay();
			uut2 = new InstructionDisplay();
		}

		[TestCase("test")]
		[TestCase("nytest")]
		[TestCase("123test")]
		public void ChargeDisplayTextIsCorrect(string input)
		{
			uut1.Print(input);
			Assert.That(uut1.printText, Is.EqualTo(input));
		}

		[TestCase("test")]
		[TestCase("nytest")]
		[TestCase("123test")]
		public void InstructionDisplayTextIsCorrect(string input)
		{
			uut2.Print(input);
			Assert.That(uut1.printText, Is.EqualTo(input));
		}

	}
}
