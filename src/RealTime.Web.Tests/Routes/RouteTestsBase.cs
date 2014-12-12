namespace RealTime.Web.Tests.Routes
{
   using System.Web;
   using System.Web.Routing;

   using FakeItEasy;

   using NUnit.Framework;

   public abstract class RouteTestsBase
   {
      protected static RouteData GetRouteData(string url)
      {
         var routes = new RouteCollection();
         RouteConfig.RegisterRoutes(routes);

         var httpContext = A.Fake<HttpContextBase>();

         A.CallTo(() => httpContext.Request.AppRelativeCurrentExecutionFilePath).Returns(url);

         var routeData = routes.GetRouteData(httpContext);

         Assert.IsNotNull(routeData);

         routeData.Values["controller"] = routeData.Values["controller"].ToString();
         routeData.Values["action"] = routeData.Values["action"].ToString();

         return routeData;
      }
   }
}
