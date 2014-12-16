namespace RealTime.Domain.Persistence.RavenDb
{
   using System;
   using System.Linq;
   using System.Linq.Expressions;

   using Raven.Client;
   using Raven.Client.Linq;

   public class RavenDbDocumentStore : IStoreDocuments
   {
      private readonly IDocumentStore documentStore;

      public RavenDbDocumentStore(IDocumentStore documentStore)
      {
         this.documentStore = documentStore;
      }

      public void Store<TDocument>(TDocument document) where TDocument : class
      {
         if (document == null)
         {
            throw new ArgumentNullException("document");
         }

         using (var session = this.documentStore.OpenSession())
         {
            session.Store(document);
            session.SaveChanges();
         }
      }

      public TDocument Retrieve<TDocument>(Guid id) where TDocument : class
      {
         if (id == Guid.Empty)
         {
            throw new ArgumentException("id");
         }

         using (var session = this.documentStore.OpenSession())
         {
            return session.Load<TDocument>(id);
         }
      }

      // TODO: Write tests to cover this
      public T[] Query<T>(Expression<Func<T, bool>> predicate)
      {
         using (var session = this.documentStore.OpenSession())
         {
            return session.Query<T>().Where(predicate).ToArray();
         }
      }
   }
}
