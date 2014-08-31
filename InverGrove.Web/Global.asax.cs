using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;
using InverGrove.Data;
using InverGrove.Domain.Factories;

namespace InverGrove.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly IWindsorContainer container = new WindsorContainer();

        protected void Application_Start()
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ContainerConfig.RegisterTypes(container);

            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory(container));

            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<InverGroveContext>());
        }
    }
}
