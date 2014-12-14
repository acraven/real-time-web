namespace RealTime.ServiceBus
{
   public interface IServiceBus
   {
      void Subscribe<TMessage, TMessageHandler>()
         where TMessage : class
         where TMessageHandler : class, IMessageHandler<TMessage>;

      void Publish<TMessage>(TMessage message) where TMessage : class;
   }
}
