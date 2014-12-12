namespace RealTime.Domain.Persistence
{
   public interface IStoreDocuments
   {
      void Store<TDocument>(TDocument document) where TDocument : class;
   }
}
