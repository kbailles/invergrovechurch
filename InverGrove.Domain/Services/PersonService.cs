using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        public IPerson GetById(int personId)
        {
            var person = this.personRepository.Get(x => x.PersonId == personId, includeProperties: "PhoneNumbers").FirstOrDefault();

            return person.ToModel();
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
            Guid acccessToken;

            var personId = this.personRepository.Add(person);

            if (person.IsUser && (personId > 0))
            {
                acccessToken = this.verificationRepository.Add(personId);
            }

            return personId;
        }


        public int Edit(IPerson person)
        {

            var foo = "hello world";
            return 0;

            //var isDeleted = this.personRepository.Delete(person);

            //if (isDeleted)
            //{
            //    return person.PersonId;
            //}
            //else
            //{
            //    return 0;
            //}
        }


        /// <summary>
        /// Deletes the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <returns></returns>
        public int Delete(IPerson person)
        {
            var isDeleted = this.personRepository.Delete(person);

            if (isDeleted)
            {
                return person.PersonId;
            }
            else
            {
                return 0;          
            }
        }
    }
}