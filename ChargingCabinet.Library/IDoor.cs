using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChargingCabinet.Library
{
   public class DoorOpenedEventArgs : EventArgs
   {
      public bool State { get; set; }
   }

   public class DoorClosedEventArgs : EventArgs
   {
      public bool State { get; set; }
   }

   public interface IDoor
    {
       public event EventHandler<DoorOpenedEventArgs> DoorOpenedEvent;

       public event EventHandler<DoorClosedEventArgs> DoorClosedEvent;

       public void LockDoor();

       public void UnlockDoor();

    }
}
