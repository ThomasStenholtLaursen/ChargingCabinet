using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Library
{
   public class Door : IDoor
    {
       public event EventHandler<DoorOpenedEventArgs> DoorOpenedEvent;
       public event EventHandler<DoorClosedEventArgs> DoorClosedEvent;

       private bool DoorState { get; set; }

       public void LockDoor()
       {
          DoorState = false;
       }

       public void UnlockDoor()
       {
          DoorState = true;
       }

       public void OnDoorOpened()
       {
          DoorOpenedEvent?.Invoke(this, new DoorOpenedEventArgs(){State = true});
       }

       public void OnDoorClosed()
       {
          DoorClosedEvent?.Invoke(this, new DoorClosedEventArgs(){State = false});
       }
    }
}
