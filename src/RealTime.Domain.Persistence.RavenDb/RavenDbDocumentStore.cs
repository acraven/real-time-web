namespace RealTime.Domain.Persistence.RavenDb
{
   using System;

   using Raven.Client;

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
   }
}
