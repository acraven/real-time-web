namespace RealTime.Web.Tests.Routes
{
   using NUnit.Framework;

   public class PriceRoutesTests : RouteTestsBase
   {
      [Test]
      [Ignore]
      public void IndexActionIsResolved()
      {
         var routeData = GetRouteData("~/Prices/C35814F0-B47E-4DA4-BBC1-3DFEF02130F9");

         Assert.That(routeData.Values["controller"], Is.EqualTo("Prices"));
         Assert.That(routeData.Values["action"], Is.EqualTo("Index"));
         Assert.That(routeData.Values["requestId"], Is.EqualTo("C35814F0-B47E-4DA4-BBC1-3DFEF02130F9"));
      }
   }
}
