using NUnit.Framework;

namespace ChargingCabinet.Test
{
   [TestFixture]
   class RFIDUnitTest
   {
      private RFIDReader _utt;
      private RFIDEventArgs _rfidEvent;

      [SetUp]
      public void Setup()
      {
         _rfidEvent = null;
         _utt = new RFIDReader();
         _utt.RFIDDetectedEvent += (sender, args) => { _rfidEvent = args; };
      }

      [Test]
      public void SetRFID_Event_Fired()
      {
         _utt.SetRFID(10);

         Assert.That(_rfidEvent, Is.Not.Null);
      }

      [Test]
      public void SetRFID_NewValue_Corrected()
      {
         _utt.SetRFID(10);

         Assert.That(_rfidEvent.Detected, Is.EqualTo(10));
      }
   }
}