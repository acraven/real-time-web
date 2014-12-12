namespace RealTime.Web.Tests.Controllers.PricesControllerTests
{
   using System;
   using System.Web.Mvc;

   using FakeItEasy;

   using NUnit.Framework;

   using RealTime.Domain;
   using RealTime.Web.Controllers;

   public class RequestPricesShould
   {
      private IRequestPrices requestPrices;

      private PricesController controller;

      [SetUp]
      public void SetupBeforeEachTest()
      {
         this.requestPrices = A.Fake<IRequestPrices>();

         this.controller = new PricesController(this.requestPrices);
      }

      [Test]
      public void CallRequestOnRequestPrices()
      {
         this.controller.RequestPrices();

         A.CallTo(() => this.requestPrices.Request()).MustHaveHappened(Repeated.Exactly.Once);
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

         Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());

         Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
         Assert.That(result.RouteValues["controller"], Is.Null);
      }

      [Test]
      public void ReturnResultOfRequestPricesAsRouteParameter()
      {
         var requestId = Guid.NewGuid();

         A.CallTo(() => this.requestPrices.Request()).Returns(requestId);

         var result = (RedirectToRouteResult)this.controller.RequestPrices();

         Assert.That(result.RouteValues["requestId"], Is.EqualTo(requestId));
      }
   }
}
