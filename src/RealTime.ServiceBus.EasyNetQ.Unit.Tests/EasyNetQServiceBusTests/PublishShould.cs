namespace RealTime.ServiceBus.EasyNetQ.Unit.Tests.EasyNetQServiceBusTests
{
   using global::EasyNetQ;

   using FakeItEasy;

   using NUnit.Framework;

   public class PublishShould
   {
      private IBus bus;
      private IMessageHandlerFactory messageHandlerFactory;

      private IServiceBus easyNetQServiceBus;

      [SetUp]
      public void SetupBeforeEachTest()
      {
         this.bus = A.Fake<IBus>();
         this.messageHandlerFactory = A.Fake<IMessageHandlerFactory>();

         this.easyNetQServiceBus = new EasyNetQServiceBus(this.bus, this.messageHandlerFactory);
      }

      [Test]
      public void ShouldCallPublishOnBus()
      {
         this.easyNetQServiceBus.Publish(new TestMessage());

         A.CallTo(() => this.bus.Publish(A<TestMessage>._)).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void ShouldCallPublishWithMessage()
      {
         var message = new TestMessage();

         this.easyNetQServiceBus.Publish(message);

         A.CallTo(() => this.bus.Publish(message)).MustHaveHappened(Repeated.Exactly.Once);
      }

      public class TestMessage
      {
      }
   }
}
