using System.Collections.Generic;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonFactory personFactory;
        private readonly IPersonRepository personRepository;
        private readonly IUserVerificationRepository verificationRepository;

        public PersonService(IPersonRepository personRepository, IPersonFactory personFactory, IUserVerificationRepository verificationRepository)
        {
            this.personRepository = personRepository;
            this.personFactory = personFactory;
            this.verificationRepository = verificationRepository;
        }

        /// <summary>
        /// Adds the person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">person</exception>
        public int AddPerson(IPerson person)
        {
            Guard.ParameterNotNull(person, "person");

            var personId = this.personRepository.Add(person);

            if (person.IsUser && (personId > 0))
            {
                // (1) Add row to UserVerification
                var isNotified = this.verificationRepository.Add(personId);

                // (2) Send email with UserVerfication ID
                // TODO -  send notification email.
                // bool hasSent = this.mailService.SendContactMail(contact);
            }

            return personId;
        }

        /// <summary>
        /// Gets the base/default person.
        /// </summary>
        /// <returns></returns>
        public IPerson GetBasePerson()
        {
            return this.personFactory.CreatePerson();
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