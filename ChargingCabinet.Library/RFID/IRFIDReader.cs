using System;
public class RFIDEventArgs : EventArgs
{
   public double Detected { set; get; }

}

public interface IRFIDReader
{
   public event EventHandler<RFIDEventArgs> RFIDDetectedEvent;

}
