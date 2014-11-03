using System;
using InverGrove.Data;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;

namespace InverGrove.Domain.Repositories
{
    public class PersonRepository : EntityRepository<Data.Entities.Person, int>, IPersonRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public PersonRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Adds the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">person</exception>
        public int Add(IPerson person)
        {
            if (person == null)
            {
                throw new ParameterNullException("person");
            }

            var currentDate = DateTime.Now;

            Data.Entities.Person personEntity = ((Person)person).ToEntity();
            personEntity.DateCreated = currentDate;
            personEntity.DateModified = currentDate;
            personEntity.Profiles = null;
            personEntity.Relatives = null;
            personEntity.Relatives1 = null;
            personEntity.MaritalStatus = null;
            personEntity.PersonType = null;

            this.Insert(personEntity);

            this.Save();

            return personEntity.PersonId;
        }
    }
}