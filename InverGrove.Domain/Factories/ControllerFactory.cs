using System;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using Castle.Windsor;
using Castle.MicroKernel;

namespace InverGrove.Domain.Factories
{
    public class ControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer container;

        public ControllerFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType == null)
            {
                throw new HttpException(404,
                    string.Format("The controller for path '{0}' could not be found or it does not implement IController.",
                        requestContext.HttpContext.Request.Path));
            }

            if (!typeof (IController).IsAssignableFrom(controllerType))
            {
                throw new ArgumentException(
                    string.Format("Type requested is not a controller: {0}", controllerType.FullName), "controllerType");
            }

            var controllerName = controllerType.FullName.ToLower();

            try
            {
                var controller = this.container.Resolve<IController>(controllerName);

                return controller;
            }
            catch (ComponentNotFoundException)
            {
                throw new HttpException(404, "The controller " + controllerName + " was not found");
            }
        }

        public override void ReleaseController(IController controller)
        {
            this.container.Release(controller);
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }
    }
}
