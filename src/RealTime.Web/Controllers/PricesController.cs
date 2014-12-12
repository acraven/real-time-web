namespace RealTime.Web.Controllers
{
   using System;
   using System.Web.Mvc;

   using RealTime.Domain;

   public class PricesController : Controller
   {
      private readonly IRequestPrices requestPrices;

      public PricesController(IRequestPrices requestPrices)
      {
         this.requestPrices = requestPrices;
      }

      [Route("prices/{requestId}")] 
      public ActionResult Index(Guid requestId)
      {
         return this.View();
      }

      [HttpPost]
      [Route("prices/request-prices")]
      public ActionResult RequestPrices()
      {
         var requestId = this.requestPrices.Request();

         return this.RedirectToAction("Index", new { requestId });
      }
   }
}