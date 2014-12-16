namespace RealTime.Web.Tests.Controllers.PricesControllerTests
{
   using System;
   using System.Web.Mvc;

   using FakeItEasy;

   using NUnit.Framework;

   using RealTime.Core;
   using RealTime.Domain.Persistence;
   using RealTime.Messages.Commands;
   using RealTime.ServiceBus;
   using RealTime.Web.Controllers;

   public class RequestPricesShould
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
      public void CallCreateOnGuidFactory()
      {
         this.controller.RequestPrices();

         A.CallTo(() => this.guidFactory.Create()).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void CallPublishRequestPricesMessage()
      {
         this.controller.RequestPrices();

         A.CallTo(() => this.serviceBus.Publish(A<RequestPrices>._)).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void CallPublishRequestPricesMessageWithRequestId()
      {
         var requestId = Guid.NewGuid();

         A.CallTo(() => this.guidFactory.Create()).Returns(requestId);

         this.controller.RequestPrices();

         A.CallTo(() => this.serviceBus.Publish(A<RequestPrices>.That.Matches(c => c.Id == requestId))).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void ReturnInstanceOfRedirectToRouteResult()
      {
         var result = this.controller.RequestPrices();

         Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
      }

      [Test]
      public void ReturnInstanceOfRedirectToRouteResultContainingIndexAction()
      {
         var result = (RedirectToRouteResult)this.controller.RequestPrices();

         Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
         Assert.That(result.RouteValues["controller"], Is.Null);
      }

      [Test]
      public void ReturnRequestIdAsRouteParameter()
      {
         var requestId = Guid.NewGuid();

         A.CallTo(() => this.guidFactory.Create()).Returns(requestId);

         var result = (RedirectToRouteResult)this.controller.RequestPrices();

         Assert.That(result.RouteValues["requestId"], Is.EqualTo(requestId));
      }
   }
}
