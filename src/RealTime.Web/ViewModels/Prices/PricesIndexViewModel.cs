namespace RealTime.Web.ViewModels.Prices
{
   public class PricesIndexViewModel
   {
      public Price[] Prices { get; set; }

      public class Price
      {
         public int Sequence { get; set; }
      }
   }
}