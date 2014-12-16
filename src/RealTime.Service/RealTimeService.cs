namespace RealTime.Service
{
   using Castle.Windsor;

   using RealTime.Domain.EventHandlers;
   using RealTime.Messages.Commands;
   using RealTime.Messages.Events;
   using RealTime.ServiceBus;

   public class RealTimeService
   {
      private IWindsorContainer container;

      public void Start()
      {
         this.container = ContainerConfig.RegisterContainer();

         var serviceBus = this.container.Resolve<IServiceBus>();
         serviceBus.Subscribe<PriceAvailable, PriceAvailableHandler>();
         serviceBus.Subscribe<RequestPrices, RequestPricesHandler>();
      }

      public void Stop()
      {
         this.container.Dispose();
         this.container = null;
      }
   }
}