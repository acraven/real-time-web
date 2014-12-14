namespace RealTime.Core.DependencyInjection
{
   public interface IResolveDependencies
   {
      TComponent Resolve<TComponent>() where TComponent : class;
   }
}
