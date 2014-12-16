namespace RealTime.Messages.Events
{
   using System;

   public class PriceAvailable
   {
      public Guid RequestId { get; set; }

      public int Sequence { get; set; }

      public DateTime CreatedDateTime { get; set; }
   }
}
