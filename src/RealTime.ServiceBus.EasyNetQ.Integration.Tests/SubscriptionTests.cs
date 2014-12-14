namespace RealTime.ServiceBus.EasyNetQ.Integration.Tests
{
   using System.Collections.Concurrent;
   using System.Linq;
   using System.Threading;

   using global::EasyNetQ;

   using FakeItEasy;

   using NUnit.Framework;

   public class SubscriptionTests
   {
      [Test]
      public void MultipleMessagesAreHandled()
      {
         const int Messages = 20;

         var sequences = new ConcurrentBag<int>();

         var messageHandlerFactory = A.Fake<IMessageHandlerFactory>();
         var messageHandler = new TestMessageHandler(sequences);
         A.CallTo(() => messageHandlerFactory.Create<TestMessage, TestMessageHandler>()).Returns(messageHandler);

         using (var bus = RabbitHutch.CreateBus("host=localhost"))
         {
            var serviceBus = new EasyNetQServiceBus(bus, messageHandlerFactory);

            serviceBus.Subscribe<TestMessage, TestMessageHandler>();

            for (var i = 0; i < Messages; i++)
            {
               serviceBus.Publish(new TestMessage { Sequence = i });
            }

            var j = 0;
            while (j < 10 && sequences.Count < Messages)
            {
               Thread.Sleep(500);
               j++;
            }
         }

         Assert.AreEqual(Messages, sequences.Count);
         Assert.AreEqual(Messages, sequences.Distinct().Count());
      }

      public class TestMessage
      {
         public int Sequence { get; set; }
      }

      private class TestMessageHandler : IMessageHandler<TestMessage>
      {
         private readonly ConcurrentBag<int> sequences;

         public TestMessageHandler(ConcurrentBag<int> sequences)
         {
            this.sequences = sequences;
         }

         public void Handle(TestMessage message)
         {
            this.sequences.Add(message.Sequence);
         }
      }
   }
}
