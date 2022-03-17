using NUnit.Framework;
using ChargingCabinet.Library;
using NSubstitute;

namespace ChargingCabinet.Test
{
    public class ChargeControlTest
    {
        private IChargeControl _uut;
        private IUsbCharger _usbCharger;

        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _uut = new ChargeControl(_usbCharger);
        }

        [TestCase(true,true)]
        [TestCase(false, false)]
        public void IsConnected_Method_TestsCorrectReturn(bool input,bool output)
        {
            _uut.Connected = input;
            Assert.That(_uut.Connected, Is.EqualTo(output));
        }

        [Test]
        public void StopChargeTest_CallsUsbChargerCorrectly()
        {
            _uut.StopCharge();
            _usbCharger.Received(1).StopCharge();
        }

        [Test]
        public void StartChargeTest_CallsUsbChargerCorrectly()
        {
            _uut.StartCharge();
            _usbCharger.Received(1).StartCharge();
        }
    }
}