using InverGrove.Data.Entities;

namespace InverGrove.Domain.Interfaces
{
    public interface IPersonRepository : IEntityRepository<Person, int>
    {

        /// <summary>
        /// Adds the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">person</exception>
        int Add(IPerson person);

        /// <summary>
        /// Updates the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        IPerson Update(IPerson person);

        /// <summary>
        /// Deletes the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        bool Delete(IPerson person);
    }
}