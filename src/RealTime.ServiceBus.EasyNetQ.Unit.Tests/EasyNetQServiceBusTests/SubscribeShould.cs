namespace RealTime.ServiceBus.EasyNetQ.Unit.Tests.EasyNetQServiceBusTests
{
   using System;

   using global::EasyNetQ;

   using FakeItEasy;

   using NUnit.Framework;

   using RealTime.Core.DependencyInjection;

   public class SubscribeShould
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
      public void ShouldCallSubscribeOnBus()
      {
         this.easyNetQServiceBus.Subscribe<TestMessage, TestMessageHandler>();

         A.CallTo(() => this.bus.Subscribe(A<string>._, A<Action<TestMessage>>._)).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void ShouldCallSubscribeWithNameOfHandler()
      {
         const string ExpectedSubscriptionId = "RealTime.ServiceBus.EasyNetQ.Unit.Tests.EasyNetQServiceBusTests.SubscribeShould+TestMessageHandler";

         this.easyNetQServiceBus.Subscribe<TestMessage, TestMessageHandler>();

         A.CallTo(() => this.bus.Subscribe(ExpectedSubscriptionId, A<Action<TestMessage>>._)).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void ShouldCallSubscribeWithAction()
      {
         this.easyNetQServiceBus.Subscribe<TestMessage, TestMessageHandler>();

         A.CallTo(() => this.bus.Subscribe(A<string>._, A<Action<TestMessage>>.That.Not.IsNull())).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void ShouldCallCreateMessageHandlerFactoryOnSubscribeAction()
      {
         var action = this.SubscribeAction();

         action(new TestMessage());

         A.CallTo(() => this.resolveDependencies.Resolve<TestMessageHandler>()).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void ShouldCallMessageHandlerWithMessage()
      {
         var action = this.SubscribeAction();
         var message = new TestMessage();
         var messageHandler = A.Fake<TestMessageHandler>();

         A.CallTo(() => this.resolveDependencies.Resolve<TestMessageHandler>()).Returns(messageHandler);

         action(message);

         A.CallTo(() => messageHandler.Handle(message)).MustHaveHappened(Repeated.Exactly.Once);
      }

      private Action<TestMessage> SubscribeAction()
      {
         Action<TestMessage> action = null;

         A.CallTo(() => this.bus.Subscribe(A<string>._, A<Action<TestMessage>>._)).Invokes(c => { action = (Action<TestMessage>)c.Arguments[1]; });

         this.easyNetQServiceBus.Subscribe<TestMessage, TestMessageHandler>();

         return action;
      }

      public class TestMessage
      {
      }

      public abstract class TestMessageHandler : IMessageHandler<TestMessage>
      {
         public virtual void Handle(TestMessage message)
         {
         }
      }
   }
}
