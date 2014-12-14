namespace RealTime.Domain.Persistence
{
   using System;

   public interface IStoreDocuments
   {
      void Store<TDocument>(TDocument document) where TDocument : class;

      TDocument Retrieve<TDocument>(Guid id) where TDocument : class;
   }
}
