using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using System.Web.SessionState;
using Castle.Windsor;
using Castle.MicroKernel;

namespace InverGrove.Domain.Factories
{
    public class ControllerFactory : IControllerFactory 
    {
        private readonly IWindsorContainer container;

        public ControllerFactory(IWindsorContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Creates the specified controller by using the specified request context.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <returns>
        /// The controller.
        /// </returns>
        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            try
            {
                controllerName = controllerName.ToLower() + "controller";
                var controller = this.container.Resolve<IController>(controllerName);

                return controller;
            }
            catch (ComponentNotFoundException) 
            {
                throw new HttpException(404, "The controller " + controllerName + " was not found");
            }
        }

        public void ReleaseController(IController controller)
        {
            this.container.Release(controller);
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

    }
}
