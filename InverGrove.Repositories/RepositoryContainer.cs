using Castle.Windsor;
using Castle.MicroKernel.Registration;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Repositories
{
    public static class RepositoryContainer
    {
        public static void RegisterTypes(IWindsorContainer container)
        {
            container.Register(Component.For<IDataContextFactory>().ImplementedBy<DataContextFactory>().LifeStyle.PerWebRequest);
            //container.Register(Component.For<IPersonRepository>().ImplementedBy<PersonRepository>().LifeStyle.Transient);
            container.Register(Component.For<IAttendanceRepository>().ImplementedBy<AttendanceRepository>().LifeStyle.PerWebRequest);
            //container.Register(Component.For<IUserRepository>().ImplementedBy<UserRepository>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IContactRepository>().ImplementedBy<ContactRepository>().LifeStyle.PerWebRequest);

            //container.Register(Component.For<IProfileRepository>().ImplementedBy<ProfileRepository>().LifeStyle.PerWebRequest);

        }
    }
}
