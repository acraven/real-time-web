namespace RealTime.Web
{
   using System.Web.Mvc;
   using System.Web.Routing;

   using Castle.Windsor;

   public class ControllerFactory : DefaultControllerFactory
   {
      private readonly IWindsorContainer container;

      public ControllerFactory(IWindsorContainer container)
      {
         this.container = container;
      }

      public override IController CreateController(RequestContext requestContext, string controllerName)
      {
         return this.container.Resolve<IController>(controllerName + "Controller");
      }

      public override void ReleaseController(IController controller)
      {
         this.container.Release(controller);
      }
   }
}