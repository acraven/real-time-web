namespace RealTime.ServiceBus.EasyNetQ.Unit.Tests.EasyNetQServiceBusTests
{
   using global::EasyNetQ;

   using FakeItEasy;

   using NUnit.Framework;

   using RealTime.Core.DependencyInjection;

   public class PublishShould
   {
      private IBus bus;
      private IResolveDependencies resolveDependencies;

      private IServiceBus easyNetQServiceBus;

      [SetUp]
      public void SetupBeforeEachTest()
      {
         this.bus = A.Fake<IBus>();
         this.resolveDependencies = A.Fake<IResolveDependencies>();

         this.easyNetQServiceBus = new EasyNetQServiceBus(this.bus, this.resolveDependencies);
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
