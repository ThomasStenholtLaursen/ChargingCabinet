using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChargingCabinet.Library;
using NUnit.Framework;

namespace ChargingCabinet.Test
{
   class DoorUnitTestCorrect
   {
      private Door _utt;
      private DoorOpenedEventArgs _doorOpenedEvent;
      private DoorClosedEventArgs _doorClosedEvent;

      [SetUp]
      public void Setup()
      {
         _doorOpenedEvent = null;
         _doorClosedEvent = null;

         _utt = new Door();
         _utt.DoorOpenedEvent += (sender, args) => { _doorOpenedEvent = args; };

         _utt.DoorClosedEvent += (sender, args) => { _doorClosedEvent = args; };
      }

      [Test]
      public void DoorOpened_Event_Fired()
      {
         _utt.OnDoorOpened();
         Assert.That(_doorOpenedEvent, Is.Not.Null);
      }

      [Test]
      public void DoorClosed_Event_Fired()
      {
         _utt.OnDoorClosed();
         Assert.That(_doorClosedEvent, Is.Not.Null);
      }
   }
}
