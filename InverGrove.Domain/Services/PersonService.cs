using System.Collections.Generic;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;

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
            Guard.ArgumentNotNull(person, "person");

            var personId = this.personRepository.Add(person); 

            if (person.IsUser)
            {

                // TODO -  send notification email.
            }

            return personId;
        }

        /// <summary>
        /// Gets all people, regardless of active status or any conditions.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IPerson> GetAll()
        {
            var people = this.personRepository.Get();

            return people.ToModelCollection();
        }
    }
}