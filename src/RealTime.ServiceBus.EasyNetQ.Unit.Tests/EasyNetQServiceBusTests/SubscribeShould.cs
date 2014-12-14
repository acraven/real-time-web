namespace RealTime.ServiceBus.EasyNetQ.Unit.Tests.EasyNetQServiceBusTests
{
   using System;

   using global::EasyNetQ;

   using FakeItEasy;

   using NUnit.Framework;

   public class SubscribeShould
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

         A.CallTo(() => this.messageHandlerFactory.Create<TestMessage, TestMessageHandler>()).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void ShouldCallMessageHandlerWithMessage()
      {
         var action = this.SubscribeAction();
         var message = new TestMessage();
         var messageHandler = A.Fake<IMessageHandler<TestMessage>>();

         A.CallTo(() => this.messageHandlerFactory.Create<TestMessage, TestMessageHandler>()).Returns(messageHandler);

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

      private abstract class TestMessageHandler : IMessageHandler<TestMessage>
      {
         public void Handle(TestMessage message)
         {
            throw new NotImplementedException();
         }
      }
   }
}
