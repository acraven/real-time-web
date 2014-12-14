namespace RealTime.ServiceBus.EasyNetQ
{
   using global::EasyNetQ;

   public class EasyNetQServiceBus : IServiceBus
   {
      private readonly IBus bus;
      private readonly IMessageHandlerFactory messageHandlerFactory;

      public EasyNetQServiceBus(
         IBus bus,
         IMessageHandlerFactory messageHandlerFactory)
      {
         this.bus = bus;
         this.messageHandlerFactory = messageHandlerFactory;
      }

      public void Subscribe<TMessage, TMessageHandler>()
         where TMessage : class
         where TMessageHandler : class, IMessageHandler<TMessage>
      {
         this.bus.Subscribe<TMessage>(typeof(TMessageHandler).FullName, this.HandleMessage<TMessage, TMessageHandler>);
      }

      public void Publish<TMessage>(TMessage message) where TMessage : class
      {
         this.bus.Publish(message);
      }

      private void HandleMessage<TMessage, TMessageHandler>(TMessage message)
         where TMessage : class
         where TMessageHandler : class, IMessageHandler<TMessage>
      {
         //// TODO: Consider wrapping the following lines in some sort of context for the benefit of DI lifetime

         var messageHandler = this.messageHandlerFactory.Create<TMessage, TMessageHandler>();

         messageHandler.Handle(message);
      }
   }
}
