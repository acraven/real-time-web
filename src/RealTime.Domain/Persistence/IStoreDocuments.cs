namespace RealTime.Domain.Persistence
{
   using System;
   using System.Linq.Expressions;

   public interface IStoreDocuments
   {
      void Store<TDocument>(TDocument document) where TDocument : class;

      TDocument Retrieve<TDocument>(Guid id) where TDocument : class;

      T[] Query<T>(Expression<Func<T, bool>> predicate);
   }
}
