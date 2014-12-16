namespace RealTime.Domain.EventHandlers
{
   using System;
   using System.Threading.Tasks;

   using RealTime.Domain.Persistence;
   using RealTime.Messages.Commands;
   using RealTime.Messages.Events;
   using RealTime.ServiceBus;

   public class RequestPricesHandler : IMessageHandler<RequestPrices>
   {
      private readonly IStoreDocuments storeDocuments;
      private readonly IServiceBus serviceBus;

      public RequestPricesHandler(
         IStoreDocuments storeDocuments,
         IServiceBus serviceBus)
      {
         this.storeDocuments = storeDocuments;
         this.serviceBus = serviceBus;
      }

      public void Handle(RequestPrices message)
      {
         this.storeDocuments.Store(message);

         // Send some PriceAvailable messages to simulate sending out price requests and receiving incoming prices
         Task.Factory.StartNew(() => this.FireFakePriceAvailableMessages(message.Id));

         // TODO: Web needs to catch PriceAvailable and integrate with SignalR
      }

      private void FireFakePriceAvailableMessages(Guid requestId)
      {
         const int Messages = 30;

         Task.Delay(1000).Wait();

         for (var i = 0; i < Messages; i++)
         {
            Task.Delay(i * 20).Wait();
            this.serviceBus.Publish(new PriceAvailable { RequestId = requestId, Sequence = i, CreatedDateTime = DateTime.Now });
         }
      }
   }
}
