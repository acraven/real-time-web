namespace RealTime.Core.DependencyInjection.Castle.Unit.Tests.CastleDependencyResolverTests
{
   using global::Castle.Windsor;

   using FakeItEasy;

   using NUnit.Framework;

   public class ResolveShould
   {
      private IWindsorContainer windsorContainer;

      private IResolveDependencies castleDependencyResolver;

      public interface ITestContract
      {
      }

      [SetUp]
      public void SetupBeforeEachTest()
      {
         this.windsorContainer = A.Fake<IWindsorContainer>();

         this.castleDependencyResolver = new CastleDependencyResolver(this.windsorContainer);
      }

      [Test]
      public void CallResolveOnWindsorContainer()
      {
         this.castleDependencyResolver.Resolve<ITestContract>();

         A.CallTo(() => this.windsorContainer.Resolve<ITestContract>()).MustHaveHappened(Repeated.Exactly.Once);
      }
   }
}
