namespace RealTime.Web
{
   using System.Reflection;
   using System.Web.Mvc;

   using Castle.MicroKernel.Registration;
   using Castle.Windsor;

   using RealTime.Domain;

   public static class ContainerConfig
   {
      public static IWindsorContainer RegisterContainer()
      {
         var container = new WindsorContainer();

         RegisterControllers(container);
         RegisterComponents(container);

         return container;
      }

      private static void RegisterControllers(IWindsorContainer container)
      {
         container.Register(Classes.FromAssembly(Assembly.GetExecutingAssembly())
            .BasedOn<IController>()
            .Configure(c => c.Named(c.Implementation.Name)).LifestylePerWebRequest());
      }

      private static void RegisterComponents(WindsorContainer container)
      {
         container.Register(Component.For<IRequestPrices>().ImplementedBy<RequestPrices>().LifestyleSingleton());
      }
   }
}