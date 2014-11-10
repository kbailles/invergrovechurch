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

        ///// <summary>
        ///// Creates the specified controller by using the specified request context.
        ///// </summary>
        ///// <param name="requestContext">The request context.</param>
        ///// <param name="controllerName">The name of the controller.</param>
        ///// <returns>
        ///// The controller.
        ///// </returns> this one was used when implementing IControllerFactory, but doesn't work when trying to register other controllers
        ///  in Areas that have the same name, so using the DefaultControllerFactory inheritance instead so we can get the controllerType and resolve by full namespace name.
        //public IController CreateController(RequestContext requestContext, string controllerName)
        //{
        //    try
        //    {
        //        controllerName = controllerName.ToLower() + "controller";
        //        var controller = this.container.Resolve<IController>(controllerFullName.ToLower());

        //        return controller;
        //    }
        //    catch (ComponentNotFoundException)
        //    {
        //        throw new HttpException(404, "The controller " + controllerName + " was not found");
        //    }
        //}

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
