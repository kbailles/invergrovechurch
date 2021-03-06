﻿using System.Collections;
using System.Collections.Generic;

namespace InverGrove.Domain.Interfaces
{
    public interface IPersonService
    {

        /// <summary>
        /// Gets all people, regardless of active status or any conditions.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IPerson> GetAll();

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        IPerson GetById(int personId);

        /// <summary>
        /// Gets the base person.
        /// </summary>
        /// <returns></returns>
        IPerson GetBasePerson();

        /// <summary>
        /// Adds the person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="hostName">Name of the host.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">person</exception>
        IPerson AddPerson(IPerson person, string hostName);

        /// <summary>
        /// Edits the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        IPerson Edit(IPerson person, string hostName = "");

        /// <summary>
        /// Deletes the specified person.
        /// </summary>
        /// <param name="Person">The person.</param>
        /// <returns></returns>
        int Delete(IPerson Person);
    }
}