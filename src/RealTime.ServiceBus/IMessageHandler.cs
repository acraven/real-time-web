namespace RealTime.ServiceBus
{
   public interface IMessageHandler<in TMessage> where TMessage : class
   {
      void Handle(TMessage message);
   }
}
