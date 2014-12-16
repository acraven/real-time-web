namespace RealTime.Domain.Unit.Tests.EventHandlersTests.PriceAvailableHandlerTests
{
   using FakeItEasy;

   using NUnit.Framework;

   using RealTime.Domain.EventHandlers;
   using RealTime.Domain.Persistence;
   using RealTime.Messages.Events;

   public class HandleShould
   {
      private IStoreDocuments storeDocuments;

      private PriceAvailableHandler priceAvailableHandler;

      [SetUp]
      public void SetupBeforeEachTest()
      {
         this.storeDocuments = A.Fake<IStoreDocuments>();

         this.priceAvailableHandler = new PriceAvailableHandler(this.storeDocuments);
      }

      [Test]
      public void ShouldStorePriceAvailableMessage()
      {
         var message = new PriceAvailable();

         this.priceAvailableHandler.Handle(message);

         A.CallTo(() => this.storeDocuments.Store(message)).MustHaveHappened(Repeated.Exactly.Once);
      }
   }
}
