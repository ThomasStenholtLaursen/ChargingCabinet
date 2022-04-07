using System;
using ChargingCabinet.Library;
using ChargingCabinet.Library.Logging;
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
        private IUsbCharger _usbCharger;
        private ILog _log;

        [SetUp]
        public void Setup()
        {
            _door = Substitute.For<IDoor>();
            _instructionDisplay = Substitute.For<IDisplay>();
            _chargingDisplay = Substitute.For<IDisplay>();
            _rfidReader = Substitute.For<IRFIDReader>();
            _chargeControl = Substitute.For<IChargeControl>();
            _usbCharger = Substitute.For<IUsbCharger>();
            _log = Substitute.For<ILog>();
            _uut = new StationControl(_door, _instructionDisplay, _chargingDisplay, _rfidReader, _chargeControl, _log);
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
        public void TestofChargeBegins_and_instructionDisplayPrints_when_DoorIsLocked_with_RFID()
        {
            _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs() { State = true });
            _chargeControl.IsConnected().Returns(true);
            _door.DoorClosedEvent += Raise.EventWith(new DoorClosedEventArgs() { State = false });
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs() { Detected = 10 });

            _door.Received(1).LockDoor();
            _chargeControl.Received(1).StartCharge();
            _instructionDisplay.Received(1)
                .Print("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
        }

        [Test]
        public void TestofDeviceNotConnected()
        {
            _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs() { State = true });
            _chargeControl.IsConnected().Returns(false);
            _door.DoorClosedEvent += Raise.EventWith(new DoorClosedEventArgs() { State = false });
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs() { Detected = 10 });

            _instructionDisplay.Received(1).Print("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");

        }

        [Test]
        public void DoorUnlocksWithCorrectRFID()
        {
            _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs() { State = true });
            _chargeControl.IsConnected().Returns(true);
            _door.DoorClosedEvent += Raise.EventWith(new DoorClosedEventArgs() { State = false });
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs() { Detected = 10 });

            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs() { Detected = 10 });

            _chargeControl.Received(1).StopCharge();
            _door.Received(1).UnlockDoor();
            _instructionDisplay.Print("Tag din telefon ud af skabet og luk døren");
        }

        [Test]
        public void DoorRemainsLockedDueToWrongRFID()
        {
            _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs() { State = true });
            _chargeControl.IsConnected().Returns(true);
            _door.DoorClosedEvent += Raise.EventWith(new DoorClosedEventArgs() { State = false });
            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs() { Detected = 10 });

            _rfidReader.RFIDDetectedEvent += Raise.EventWith(new RFIDEventArgs() { Detected = 22 });

            _instructionDisplay.Print("Forkert RFID tag");
        }
    }
}