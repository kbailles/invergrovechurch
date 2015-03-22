using System.Collections;
using System.Collections.Generic;

namespace InverGrove.Domain.Interfaces
{
    public interface IPersonService
    {
        /// <summary>
        /// Adds the person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">person</exception>
        int AddPerson(IPerson person);

        /// <summary>
        /// Gets all people, regardless of active status or any conditions.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IPerson> GetAll();

        /// <summary>
        /// Gets the base person.
        /// </summary>
        /// <returns></returns>
        IPerson GetBasePerson();
    }
}