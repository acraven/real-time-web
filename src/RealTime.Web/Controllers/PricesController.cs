namespace RealTime.Web.Controllers
{
   using System;
   using System.Linq;
   using System.Web.Mvc;

   using RealTime.Core;
   using RealTime.Domain.Persistence;
   using RealTime.Messages.Commands;
   using RealTime.Messages.Events;
   using RealTime.ServiceBus;
   using RealTime.Web.ViewModels.Prices;

   public class PricesController : Controller
   {
      private readonly IGuidFactory guidFactory;
      private readonly IServiceBus serviceBus;
      private readonly IStoreDocuments storeDocuments;

      public PricesController(
         IGuidFactory guidFactory,
         IServiceBus serviceBus,
         IStoreDocuments storeDocuments)
      {
         this.guidFactory = guidFactory;
         this.serviceBus = serviceBus;
         this.storeDocuments = storeDocuments;
      }

      public ActionResult Index(Guid requestId)
      {
         var prices = this.storeDocuments.Query<PriceAvailable>(c => c.RequestId == requestId);

         return this.View(new PricesIndexViewModel { Prices = prices.Select(this.MapPrice).ToArray() });
      }

      [HttpPost]
      public ActionResult RequestPrices()
      {
         var requestId = this.guidFactory.Create();
         this.serviceBus.Publish(new RequestPrices { Id = requestId });

         return this.RedirectToAction("Index", new { requestId });
      }

      private PricesIndexViewModel.Price MapPrice(PriceAvailable priceAvailable)
      {
         return new PricesIndexViewModel.Price { Sequence = priceAvailable.Sequence };
      }
   }
}