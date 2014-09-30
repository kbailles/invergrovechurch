using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using InverGrove.Data;
using InverGrove.Domain.Factories;
using Invergrove.Domain.Interfaces;
using InverGrove.Domain.Models;

namespace InverGrove.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly IWindsorContainer container = new WindsorContainer();

        protected void Application_Start()
        {
            // re-add the registger areas if we use areas.
            GlobalConfiguration.Configure(WebApiConfig.Register);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ContainerConfig.RegisterTypes(container);

            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory(container));

            Database.SetInitializer(new InverGroveInitializer());

            this.ForceDbCreation();
        }

        private void ForceDbCreation()
        {
#if DEBUG
            var roles = container.Resolve<IRepository<Role>>();
            roles.GetAll<string>(null);
#endif
        }
    }
}
