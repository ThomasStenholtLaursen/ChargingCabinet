using System;
using ChargingCabinet.Library;
using NSubstitute;
using NUnit.Framework;

namespace ChargingCabinet.Test
{
    [TestFixture]
    class StationControlTest
    {
        private StationControl _utt;
        private IDoor _door;
        private IDisplay _instructionDisplay;
        private IDisplay _chargingDisplay;
        private IRFIDReader _rfidReader;
        private IChargeControl _chargeControl;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _instructionDisplay = Substitute.For<IDisplay>();
            _chargingDisplay = Substitute.For<IDisplay>();
            _rfidReader = Substitute.For<IRFIDReader>();
            _chargeControl = Substitute.For<IChargeControl>();
            _utt = new StationControl(_door, _instructionDisplay, _chargingDisplay, _rfidReader, _chargeControl);
        }


        [Test]
        public void TestDoorOpenedEventStateChange()
        {
            _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs() { State = true });
            Assert.That(_utt.State, Is.EqualTo(true));
        }

        [Test]
        public void TestDoorClosedEventStateChange()
        {
            _door.DoorClosedEvent += Raise.EventWith(new DoorClosedEventArgs() { State = false });
            Assert.That(_utt.State, Is.EqualTo(false));
        }

        [Test]
        public void TestRFidReaderDetectsNewId()
        {
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs() { Detected = 10 });
            Assert.That(_utt._newId, Is.EqualTo(10));
        }
    }
}