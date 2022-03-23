using NUnit.Framework;
using ChargingCabinet.Library;
using NSubstitute;

namespace ChargingCabinet.Test
{
    public class ChargeControlTest
    {
        private IChargeControl _uut;
        private IUsbCharger _usbCharger;
        private IDisplay _display;

        [SetUp]
        public void Setup()
        {
            _usbCharger = Substitute.For<IUsbCharger>();
            _display = Substitute.For<IDisplay>();
            _uut = new ChargeControl(_usbCharger, _display);
        }

        [TestCase(true, true)]
        [TestCase(false, false)]
        public void IsConnected_Method_TestsCorrectReturn(bool input, bool output)
        {
            _uut.Connected = input;
            Assert.That(_uut.IsConnected(), Is.EqualTo(output));
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

        //[TestCase(0, null, false, 0, 0)]
        [TestCase(5, "Device is fully charged!",true,1,0)]
        [TestCase(3, "Device is fully charged!", true,1,0)]
        [TestCase(10, "Device is charging!", true,0,0)]
        [TestCase(500, "Device is charging!", true,0,0)]
        [TestCase(501, "Error!", null,1,0)]
        [TestCase(1000, "Error!", null,1,0)]
        public void TestHandleNewCurrent_Method_PropertiesChanged_and_ChargeCommandsCalled(double current, string displayText, bool connection, int stop, int start)
        {

            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });
            
            Assert.That(_uut.Connected,Is.EqualTo(connection));
            _display.Received(1).Print(displayText);
            _usbCharger.Received(stop).StopCharge();
            _usbCharger.Received(start).StartCharge();
        }

        [TestCase(0, false)]
        public void TestHandleNewCurrent_whereCurrentIsZero(double current, bool connection)
        {
            _usbCharger.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs { Current = current });
            Assert.That(_uut.Connected, Is.EqualTo(connection));
        }
    }
}

