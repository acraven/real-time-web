namespace RealTime.ServiceBus
{
   public interface IMessageHandlerFactory
   {
      IMessageHandler<TMessage> Create<TMessage, TMessageHandler>()
         where TMessage : class
         where TMessageHandler : class, IMessageHandler<TMessage>;
   }
}
