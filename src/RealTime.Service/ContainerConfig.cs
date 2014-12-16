namespace RealTime.Service
{
   using Castle.MicroKernel.Registration;
   using Castle.Windsor;

   using EasyNetQ;

   using Raven.Client;
   using Raven.Client.Document;

   using RealTime.Core;
   using RealTime.Core.DependencyInjection;
   using RealTime.Core.DependencyInjection.Castle;
   using RealTime.Domain.EventHandlers;
   using RealTime.Domain.Persistence;
   using RealTime.Domain.Persistence.RavenDb;
   using RealTime.ServiceBus;
   using RealTime.ServiceBus.EasyNetQ;

   public static class ContainerConfig
   {
      public static IWindsorContainer RegisterContainer()
      {
         var container = new WindsorContainer();

         RegisterComponents(container);

         return container;
      }

      private static void RegisterComponents(IWindsorContainer container)
      {
         var dependencyResolver = new CastleDependencyResolver(container);

         container.Register(Component.For<IResolveDependencies>().Instance(dependencyResolver).LifestyleSingleton());
         container.Register(Component.For<IGuidFactory>().ImplementedBy<GuidFactory>().LifestyleSingleton());
         container.Register(Component.For<IBus>().Instance(CreateEasyNetQBus()).LifestyleSingleton());
         container.Register(Component.For<IServiceBus>().ImplementedBy<EasyNetQServiceBus>().LifestyleSingleton());
         container.Register(Component.For<IDocumentStore>().Instance(CreateRavenDbDocumentStore()).LifestyleSingleton());
         container.Register(Component.For<IStoreDocuments>().ImplementedBy<RavenDbDocumentStore>().LifestyleSingleton());

         container.Register(Component.For<RequestPricesHandler>().ImplementedBy<RequestPricesHandler>().LifestyleTransient());
         container.Register(Component.For<PriceAvailableHandler>().ImplementedBy<PriceAvailableHandler>().LifestyleTransient());
      }

      private static IBus CreateEasyNetQBus()
      {
         return RabbitHutch.CreateBus("host=localhost");
      }

      private static IDocumentStore CreateRavenDbDocumentStore()
      {
         var documentStore = new DocumentStore { ConnectionStringName = "RavenDbDocumentStore" };
         documentStore.Initialize();

         return documentStore;
      }
   }
}