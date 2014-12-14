namespace RealTime.Domain.Persistence.RavenDb.Integration.Tests
{
   using System;

   using NUnit.Framework;

   using Raven.Client.Document;

   public class PersistenceTests
   {
      [Test]
      public void DocumentIsStoredAndRetrieved()
      {
         var id = Guid.NewGuid();
         var document = new TestDocument { Id = id, Value = "Hello World!" };
         TestDocument loadedDocument;

         using (var ravenDocumentStore = new DocumentStore { ConnectionStringName = "RavenDbDocumentStore" })
         {
            ravenDocumentStore.Initialize();

            var documentStore = new RavenDbDocumentStore(ravenDocumentStore);

            documentStore.Store(document);

            loadedDocument = documentStore.Retrieve<TestDocument>(id);
         }

         Assert.That(loadedDocument, Is.Not.SameAs(document));
         Assert.That(loadedDocument.Id, Is.EqualTo(document.Id));
         Assert.That(loadedDocument.Value, Is.EqualTo(document.Value));
      }

      public class TestDocument
      {
         public Guid Id { get; set; }

         public string Value { get; set; }
      }
   }
}
