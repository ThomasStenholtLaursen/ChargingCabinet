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


      [SetUp]
      public void Setup()
      {
         _door = Substitute.For<IDoor>();
         _instructionDisplay = Substitute.For<IDisplay>();
         _chargingDisplay = Substitute.For<IDisplay>();
         _utt = new StationControl(_door, _instructionDisplay, _chargingDisplay);
      }

      [TestCase(false, false)]
      [TestCase(true, true)]
      public void TestDoorOpenedEventStateChange(bool newState, bool result)
      {
         _door.DoorOpenedEvent += Raise.EventWith(new DoorOpenedEventArgs() {State = newState});
         Assert.That(_utt.State, Is.EqualTo(result));
      }
   }
}