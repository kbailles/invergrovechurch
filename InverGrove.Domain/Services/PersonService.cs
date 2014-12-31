using System.Collections.Generic;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository personRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        /// <param name="personRepository">The person repository.</param>
        public PersonService(IPersonRepository personRepository)
        {
            this.personRepository = personRepository;
        }

        /// <summary>
        /// Adds the person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">person</exception>
        public int AddPerson(IPerson person)
        {
            if (person == null)
            {
                throw new ParameterNullException("person");
            }

            var personId = this.personRepository.Add(person);

            return personId;
        }


        public IEnumerable<IPerson> GetAll()
        {
            var people = this.personRepository.Get();

            return people.ToModelCollection();
        }
    }
}