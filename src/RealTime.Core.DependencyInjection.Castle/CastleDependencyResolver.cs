namespace RealTime.Core.DependencyInjection.Castle
{
   using global::Castle.Windsor;

   using RealTime.Core.DependencyInjection;

   public class CastleDependencyResolver : IResolveDependencies
   {
      private readonly IWindsorContainer windsorContainer;

      public CastleDependencyResolver(IWindsorContainer windsorContainer)
      {
         this.windsorContainer = windsorContainer;
      }

      public TComponent Resolve<TComponent>() where TComponent : class
      {
         return this.windsorContainer.Resolve<TComponent>();
      }
   }
}
