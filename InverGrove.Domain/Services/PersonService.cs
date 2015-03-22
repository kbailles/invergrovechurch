﻿using System.Collections.Generic;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;
using InverGrove.Domain.Models;
using InverGrove.Domain.Factories;

namespace InverGrove.Domain.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonFactory personFactory;
        private readonly IPersonRepository personRepository;
        

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService"/> class.
        /// </summary>
        /// <param name="personRepository">The person repository.</param>
        public PersonService(IPersonRepository personRepository, IPersonFactory personFactory)
        {
            this.personRepository = personRepository;
            this.personFactory = personFactory;
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

            if (person.IsUser)
            {
                // (1) Add row to UserVerification

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