using System;
using ChargingCabinet.Library;
using NSubstitute;
using NUnit.Framework;

namespace ChargingCabinet.Test
{
    [TestFixture]
    class StationControlTest
    {
        private StationControl _uut;
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
            _uut = new StationControl(_door, _instructionDisplay, _chargingDisplay, _rfidReader, _chargeControl);
        }


        [Test]
        public void TestDoorOpenedEventStateChange()
        {
            _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs() { State = true });
            Assert.That(_uut.State, Is.EqualTo(true));
        }

        [Test]
        public void TestDoorClosedEventStateChange()
        {
            _door.DoorClosedEvent += Raise.EventWith(new DoorClosedEventArgs() { State = false });
            Assert.That(_uut.State, Is.EqualTo(false));
        }

        [Test]
        public void TestRFidReaderDetectsNewId()
        {
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs() { Detected = 10 });
            Assert.That(_uut._newId, Is.EqualTo(10));
        }

        [Test]
        public void TestRFidDetected_StateIsAvailable()
        {
            _uut._state = StationControl.ChargingCabinet.Available;

            _chargeControl.Connected = true;

            
            _uut.RfidDetected(10);

            _door.Received(1).LockDoor();
            _chargeControl.Received(1).StartCharge();
            Assert.That(_uut._oldId, Is.EqualTo(10));
            _instructionDisplay.Received().Print("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
            Assert.That(_uut._state, Is.EqualTo(StationControl.ChargingCabinet.Locked));
        }
    }
}