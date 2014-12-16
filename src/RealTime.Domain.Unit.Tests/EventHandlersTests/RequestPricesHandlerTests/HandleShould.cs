namespace RealTime.Domain.Unit.Tests.EventHandlersTests.RequestPricesHandlerTests
{
   using System;

   using FakeItEasy;

   using NUnit.Framework;

   using RealTime.Core;
   using RealTime.Domain.EventHandlers;
   using RealTime.Domain.Persistence;
   using RealTime.Messages.Commands;
   using RealTime.ServiceBus;

   public class HandleShould
   {
      private IStoreDocuments storeDocuments;
      private IServiceBus serviceBus;

      private RequestPricesHandler requestPricesHandler;

      [SetUp]
      public void SetupBeforeEachTest()
      {
         this.storeDocuments = A.Fake<IStoreDocuments>();
         this.serviceBus = A.Fake<IServiceBus>();

         this.requestPricesHandler = new RequestPricesHandler(this.storeDocuments, this.serviceBus);
      }

      [Test]
      public void ShouldStoreRequestPricesMessage()
      {
         var message = new RequestPrices();

         this.requestPricesHandler.Handle(message);

         A.CallTo(() => this.storeDocuments.Store(message)).MustHaveHappened(Repeated.Exactly.Once);
      }
   }
}
