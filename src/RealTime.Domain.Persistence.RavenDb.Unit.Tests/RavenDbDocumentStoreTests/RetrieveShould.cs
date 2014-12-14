namespace RealTime.Domain.Persistence.RavenDb.Unit.Tests.RavenDbDocumentStoreTests
{
   using System;

   using FakeItEasy;

   using NUnit.Framework;

   using Raven.Client;

   public class RetrieveShould
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
      public void ThrowExceptionIfArgumentIsEmpty()
      {
         Assert.That(() => this.ravenDbDocumentStore.Retrieve<object>(Guid.Empty), Throws.InstanceOf<ArgumentException>());
      }

      [Test]
      public void CallOpenSession()
      {
         this.ravenDbDocumentStore.Retrieve<object>(Guid.NewGuid());

         A.CallTo(() => this.documentStore.OpenSession()).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void CallLoadOnOpenedSession()
      {
         A.CallTo(() => this.documentStore.OpenSession()).Returns(this.documentSession);

         this.ravenDbDocumentStore.Retrieve<object>(Guid.NewGuid());

         A.CallTo(() => this.documentSession.Load<object>(A<Guid>._)).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void CallLoadWithId()
      {
         var id = Guid.NewGuid();
         A.CallTo(() => this.documentStore.OpenSession()).Returns(this.documentSession);

         this.ravenDbDocumentStore.Retrieve<object>(id);

         A.CallTo(() => this.documentSession.Load<object>(id)).MustHaveHappened(Repeated.Exactly.Once);
      }

      [Test]
      public void ReturnDocumentFromLoad()
      {
         var document = new object();
         A.CallTo(() => this.documentStore.OpenSession()).Returns(this.documentSession);
         A.CallTo(() => this.documentSession.Load<object>(A<Guid>._)).Returns(document);

         var result = this.ravenDbDocumentStore.Retrieve<object>(Guid.NewGuid());

         Assert.That(result, Is.SameAs(document));
      }

      [Test]
      public void CallDisposeOnOpenedSession()
      {
         A.CallTo(() => this.documentStore.OpenSession()).Returns(this.documentSession);

         this.ravenDbDocumentStore.Retrieve<object>(Guid.NewGuid());

         A.CallTo(() => this.documentSession.Dispose()).MustHaveHappened(Repeated.Exactly.Once);
      }
   }
}
