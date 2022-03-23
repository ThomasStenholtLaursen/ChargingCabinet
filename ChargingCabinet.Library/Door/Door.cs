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

       public bool DoorState { get; private set; }

       public void LockDoor()
       {
          DoorState = false;
       }

       public void UnlockDoor()
       {
          DoorState = true;
       }

       public virtual void OnDoorOpened()
       {
          DoorOpenedEvent?.Invoke(this, new DoorOpenedEventArgs(){State = true});
       }

       public virtual void OnDoorClosed()
       {
          DoorClosedEvent?.Invoke(this, new DoorClosedEventArgs(){State = false});
       }
    }
}
