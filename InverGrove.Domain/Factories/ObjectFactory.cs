namespace InverGrove.Domain.Factories
{
    public class ObjectFactory
    {
        /// <summary>
        /// Creates the object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Create<T>()
            where T : new()
        {
            return new T();
        }
    }
}