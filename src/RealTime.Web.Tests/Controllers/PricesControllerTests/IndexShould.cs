namespace RealTime.Web.Tests.Controllers.PricesControllerTests
{
   using System;
   using System.Linq.Expressions;
   using System.Web.Mvc;

   using FakeItEasy;

   using NUnit.Framework;

   using RealTime.Core;
   using RealTime.Domain.Persistence;
   using RealTime.Messages.Events;
   using RealTime.ServiceBus;
   using RealTime.Web.Controllers;
   using RealTime.Web.ViewModels.Prices;

   public class IndexShould
   {
      private IGuidFactory guidFactory;
      private IServiceBus serviceBus;
      private IStoreDocuments storeDocuments;

      private PricesController controller;

      [SetUp]
      public void SetupBeforeEachTest()
      {
         this.guidFactory = A.Fake<IGuidFactory>();
         this.serviceBus = A.Fake<IServiceBus>();
         this.storeDocuments = A.Fake<IStoreDocuments>();

         this.controller = new PricesController(this.guidFactory, this.serviceBus, this.storeDocuments);
      }

      [Test]
      public void ShouldCallQueryOnStoreDocuments()
      {
         this.controller.Index(Guid.Empty);

         A.CallTo(() => this.storeDocuments.Query(A<Expression<Func<PriceAvailable, bool>>>._)).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      [Ignore]
      public void ShouldCallQueryOnStoreDocumentsWithExpressionFilteringOnRequestId()
      {
         // TODO: Extract expression, compile it and test it
         Assert.Fail();
      }

      [Test]
      public void ReturnInstanceOfViewResult()
      {
         var result = this.controller.Index(Guid.Empty);

         Assert.That(result, Is.InstanceOf<ViewResult>());
      }

      [Test]
      public void ReturnInstanceOfViewResultContainingDefaultView()
      {
         var result = (ViewResult)this.controller.Index(Guid.Empty);

         Assert.That(result.ViewName, Is.Empty);
      }

      [Test]
      public void ReturnInstanceOfViewResultContainingPricesIndexViewModel()
      {
         var result = (ViewResult)this.controller.Index(Guid.Empty);

         Assert.That(result.Model, Is.InstanceOf<PricesIndexViewModel>());
      }

      [Test]
      public void ReturnEmptyPrice()
      {
         var prices = new PriceAvailable[0];
         A.CallTo(() => this.storeDocuments.Query(A<Expression<Func<PriceAvailable, bool>>>._)).Returns(prices);

         var model = this.InvokeIndex();

         Assert.That(model.Prices.Length, Is.EqualTo(0));
      }

      [Test]
      public void ReturnOnePrice()
      {
         var prices = new[] { new PriceAvailable() };
         A.CallTo(() => this.storeDocuments.Query(A<Expression<Func<PriceAvailable, bool>>>._)).Returns(prices);

         var model = this.InvokeIndex();

         Assert.That(model.Prices.Length, Is.EqualTo(1));
      }

      [Test]
      public void ReturnTwoPrices()
      {
         var prices = new[] { new PriceAvailable(), new PriceAvailable() };
         A.CallTo(() => this.storeDocuments.Query(A<Expression<Func<PriceAvailable, bool>>>._)).Returns(prices);

         var model = this.InvokeIndex();

         Assert.That(model.Prices.Length, Is.EqualTo(2));
      }

      [Test]
      public void ReturnPriceWithSequence()
      {
         var prices = new[] { new PriceAvailable { Sequence = 1 } };
         A.CallTo(() => this.storeDocuments.Query(A<Expression<Func<PriceAvailable, bool>>>._)).Returns(prices);

         var model = this.InvokeIndex();

         Assert.That(model.Prices[0].Sequence, Is.EqualTo(1));
      }

      private PricesIndexViewModel InvokeIndex()
      {
         var result = (ViewResult)this.controller.Index(Guid.Empty);

         return (PricesIndexViewModel)result.Model;
      }
   }
}
