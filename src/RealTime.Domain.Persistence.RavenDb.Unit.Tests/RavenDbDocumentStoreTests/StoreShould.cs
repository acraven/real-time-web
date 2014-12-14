namespace RealTime.Domain.Persistence.RavenDb.Unit.Tests.RavenDbDocumentStoreTests
{
   using System;

   using FakeItEasy;

   using NUnit.Framework;

   using Raven.Client;

   public class StoreShould
   {
      private IDocumentStore documentStore;
      private IDocumentSession documentSession;

      private IStoreDocuments ravenDbDocumentStore;

      [SetUp]
      public void SetupBeforeEachTest()
      {
         this.documentStore = A.Fake<IDocumentStore>();
         this.documentSession = A.Fake<IDocumentSession>();

         this.ravenDbDocumentStore = new RavenDbDocumentStore(this.documentStore);
      }

      [Test]
      public void ThrowExceptionIfArgumentIsNull()
      {
         Assert.That(() => this.ravenDbDocumentStore.Store<object>(null), Throws.InstanceOf<ArgumentNullException>());
      }

      [Test]
      public void CallOpenSession()
      {
         this.ravenDbDocumentStore.Store(new object());

         A.CallTo(() => this.documentStore.OpenSession()).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void CallStoreOnOpenedSession()
      {
         A.CallTo(() => this.documentStore.OpenSession()).Returns(this.documentSession);

         this.ravenDbDocumentStore.Store(new object());

         A.CallTo(() => this.documentSession.Store(A<object>._)).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void CallStoreWithDocument()
      {
         var document = new object();
         A.CallTo(() => this.documentStore.OpenSession()).Returns(this.documentSession);

         this.ravenDbDocumentStore.Store(document);

         A.CallTo(() => this.documentSession.Store(document)).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void CallSaveChangesOnOpenedSession()
      {
         A.CallTo(() => this.documentStore.OpenSession()).Returns(this.documentSession);

         this.ravenDbDocumentStore.Store(new object());

         A.CallTo(() => this.documentSession.SaveChanges()).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void CallDisposeOnOpenedSession()
      {
         A.CallTo(() => this.documentStore.OpenSession()).Returns(this.documentSession);

         this.ravenDbDocumentStore.Store(new object());

         A.CallTo(() => this.documentSession.Dispose()).MustHaveHappened(Repeated.Exactly.Once);
      }
   }
}
