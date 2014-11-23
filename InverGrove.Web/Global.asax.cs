using System.Data.Entity;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using InverGrove.Data;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly IWindsorContainer container = IocFactory.Instance;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configure(WebApiConfig.Register);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ContainerConfig.RegisterTypes(container);

            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory(container));

            // WebApi Containr Resolution:
            GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator),container.Resolve<IHttpControllerActivator>());

            Database.SetInitializer(new InverGroveInitializer());

            this.ForceDbCreation();
        }

        private void ForceDbCreation()
        {
#if DEBUG
            var roles = container.Resolve<IRoleRepository>();
            roles.GetAll();
#endif
        }
    }
}
