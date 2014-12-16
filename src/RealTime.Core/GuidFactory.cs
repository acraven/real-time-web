namespace RealTime.Core
{
   using System;

   public class GuidFactory : IGuidFactory
   {
      public Guid Create()
      {
         return Guid.NewGuid();
      }
   }
}