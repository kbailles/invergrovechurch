using InverGrove.Domain.Interfaces;
using System;

namespace InverGrove.Domain.Interfaces
{
    public interface IPersonFactory
    {
        /// <summary>
        /// Creates the base/default person.
        /// </summary>
        /// <returns></returns>
        IPerson CreatePerson();
    }
}
