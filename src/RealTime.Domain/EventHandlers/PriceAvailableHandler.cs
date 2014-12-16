namespace RealTime.Domain.EventHandlers
{
   using RealTime.Domain.Persistence;
   using RealTime.Messages.Events;
   using RealTime.ServiceBus;

   public class PriceAvailableHandler : IMessageHandler<PriceAvailable>
   {
      private readonly IStoreDocuments storeDocuments;

      public PriceAvailableHandler(IStoreDocuments storeDocuments)
      {
         this.storeDocuments = storeDocuments;
      }

      public void Handle(PriceAvailable message)
      {
         this.storeDocuments.Store(message);
      }
   }
}
