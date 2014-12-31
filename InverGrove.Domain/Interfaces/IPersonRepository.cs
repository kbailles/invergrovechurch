using InverGrove.Domain.Models;
using System.Collections;
using System.Collections.Generic;

namespace InverGrove.Domain.Interfaces
{
    public interface IPersonRepository : IEntityRepository<Data.Entities.Person, int>
    {
        /// <summary>
        /// Adds the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">person</exception>
        int Add(IPerson person);

    }
}