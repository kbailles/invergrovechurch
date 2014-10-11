using Castle.Windsor;

namespace InverGrove.Domain.Factories
{
    public class IocFactory
    {
        private static readonly IWindsorContainer container = new WindsorContainer();

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static IWindsorContainer Instance
        {
            get { return container; }
        }
    }
}