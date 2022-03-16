using System;

public class RFIDReader: IRFIDReader
{
   public event EventHandler<RFIDEventArgs> RFIDDetectedEvent;

   protected virtual void OnDetection(RFIDEventArgs e)
   {
      RFIDDetectedEvent?.Invoke(this,e);
   }

   public void SetRFID(double rfid)
   {
      OnDetection(new RFIDEventArgs() {Detected = rfid});
   }
}
