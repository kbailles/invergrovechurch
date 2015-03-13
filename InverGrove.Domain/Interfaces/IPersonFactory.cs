
namespace InverGrove.Domain.Interfaces
{
    public interface IPersonFactory
    {
        /// <summary>
        /// Creates the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        bool Create(IPerson person);

    }
}