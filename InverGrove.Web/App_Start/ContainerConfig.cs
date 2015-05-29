using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using InverGrove.Data;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Providers;
using InverGrove.Domain.Repositories;
using InverGrove.Domain.Services;
using InverGrove.Web.Controllers;

namespace InverGrove.Web
{
    public class ContainerConfig
    {
        public static void RegisterTypes(IWindsorContainer container)
        {
            AddControllers(container);

            RegisterRepositories(container);
            RegisterServices(container);
            RegisterFactories(container);

            var logService = new LogService("", false);
            container.Register(Component.For<ILogService>().Instance(logService).LifeStyle.Singleton);
            container.Register(Component.For<IProfileProvider>().ImplementedBy<ProfileProvider>().LifeStyle.Transient);
            container.Register(Component.For<IMembershipProvider>().ImplementedBy<MembershipProvider>().LifeStyle.Transient);
        }

        private static void RegisterFactories(IWindsorContainer container)
        {
            var apiControllerFactory = new ApiControllerFactory(container);
            container.Register(Component.For<IHttpControllerActivator>().Instance(apiControllerFactory).LifeStyle.Singleton);
            container.Register(Component.For<IMembershipFactory>().ImplementedBy<MembershipFactory>().LifeStyle.Transient);
            container.Register(Component.For<IPersonFactory>().ImplementedBy<PersonFactory>().LifeStyle.Transient);
        }

        private static void RegisterRepositories(IWindsorContainer container)
        {
            container.Register(Component.For<IAbsentReasonRepository>().ImplementedBy<AbsentReasonRepository>().LifeStyle.Transient);
            container.Register(Component.For<IAttendanceRepository>().ImplementedBy<AttendanceRepository>().LifeStyle.Transient);
            container.Register(Component.For<IChurchRoleRepository>().ImplementedBy<ChurchRoleRepository>().LifeStyle.Transient);
            container.Register(Component.For<IContactRepository>().ImplementedBy<ContactRepository>().LifeStyle.Transient);
            container.Register(Component.For<IDataContextFactory>().ImplementedBy<DataContextFactory>().LifeStyle.Transient);
            container.Register(Component.For<IInverGroveContext>().ImplementedBy<InverGroveContext>().LifeStyle.Transient);
            container.Register(Component.For<IMaritalStatusRepository>().ImplementedBy<MaritalStatusRepository>().LifeStyle.Transient);
            container.Register(Component.For<IMembershipRepository>().ImplementedBy<MembershipRepository>().LifeStyle.Transient);
            container.Register(Component.For<IRoleRepository>().ImplementedBy<RoleRepository>().LifeStyle.Transient);
            container.Register(Component.For<ISermonRepository>().ImplementedBy<SermonRepository>().LifeStyle.Transient);
            container.Register(Component.For<IPersonRepository>().ImplementedBy<PersonRepository>().LifeStyle.Transient);
            container.Register(Component.For<IProfileRepository>().ImplementedBy<ProfileRepository>().LifeStyle.Transient);
            container.Register(Component.For<IUserVerificationRepository>().ImplementedBy<UserVerificationRepository>().LifeStyle.Transient);
            container.Register(Component.For<IUserRoleRepository>().ImplementedBy<UserRoleRepository>().LifeStyle.Transient);
            container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifeStyle.Transient);
        }

        private static void RegisterServices(IWindsorContainer container)
        {
            container.Register(Component.For<IAttendanceService>().ImplementedBy<AttendanceService>().LifeStyle.Transient);
            container.Register(Component.For<IContactService>().ImplementedBy<ContactService>().LifeStyle.Transient);
            container.Register(Component.For<IEmailService>().ImplementedBy<EmailService>().LifeStyle.Transient);
            container.Register(Component.For<IMembershipService>().ImplementedBy<MembershipService>().LifeStyle.Transient);
            container.Register(Component.For<IProfileService>().ImplementedBy<ProfileService>().LifeStyle.Transient);
            container.Register(Component.For<IRegistrationService>().ImplementedBy<RegistrationService>().LifeStyle.Transient);
            container.Register(Component.For<ISermonService>().ImplementedBy<SermonService>().LifeStyle.Transient);
            container.Register(Component.For<ISessionStateService>().ImplementedBy<SessionStateService>().LifeStyle.Transient);
            container.Register(Component.For<IPersonService>().ImplementedBy<PersonService>().LifeStyle.Transient);
            container.Register(Component.For<IUserVerificationService>().ImplementedBy<UserVerificationService>().LifeStyle.Transient);
            container.Register(Component.For<IUserService>().ImplementedBy<UserService>().LifeStyle.Transient);
            container.Register(Component.For<IRoleProvider>().ImplementedBy<RoleProvider>().LifeStyle.Transient);
        }

        private static void AddControllers(IWindsorContainer container)
        {
            // Note: use Castle, much easier to do this kind of registration !!!
            var assemblyTypes = typeof(HomeController).Assembly.GetTypes();

            foreach (var controllerType in assemblyTypes.Where(p => typeof(IController).IsAssignableFrom(p)))
            {
                container.Register(Component.For<IController>().ImplementedBy(controllerType).Named(controllerType.FullName.ToLower()).LifeStyle.Transient);
            }

            foreach (var apiControllerType in assemblyTypes.Where(a => typeof (System.Web.Http.ApiController).IsAssignableFrom(a)))
            {
                container.Register(Component.For<IHttpController>().ImplementedBy(apiControllerType).Named(apiControllerType.FullName.ToLower()).LifeStyle.Transient);
            }
        }
    }
}