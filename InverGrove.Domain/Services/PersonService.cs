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
        private readonly IEmailService emailService;
        private readonly IProfileService profileService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonService" /> class.
        /// </summary>
        /// <param name="personRepository">The person repository.</param>
        /// <param name="personFactory">The person factory.</param>
        /// <param name="verificationRepository">The verification repository.</param>
        /// <param name="emailService">The email service.</param>
        /// <param name="profileService">The profile service.</param>
        public PersonService(IPersonRepository personRepository, IPersonFactory personFactory,
            IUserVerificationRepository verificationRepository, IEmailService emailService, IProfileService profileService)
        {
            this.personRepository = personRepository;
            this.personFactory = personFactory;
            this.verificationRepository = verificationRepository;
            this.emailService = emailService;
            this.profileService = profileService;
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
        /// <param name="hostName">Name of the host.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">person</exception>
        public int AddPerson(IPerson person, string hostName)
        {
            Guard.ParameterNotNull(person, "person");

            var personId = this.personRepository.Add(person);

            if (person.IsUser && (personId > 0))
            {
                person.PersonId = personId;

                var acccessToken = this.verificationRepository.Add(personId);

                // Send email notification
                this.emailService.SendNewUserEmail(person, acccessToken, hostName);
            }

            return personId;
        }


        public int Edit(IPerson person)
        {
            Guard.ParameterNotNull(person, "person");

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
            var personProfile = this.profileService.GetProfileByPersonId(person.PersonId);

            if ((personProfile != null) && (personProfile.ProfileId > 0))
            {
                // If the person is a user, update the profile status to Disabled.
                personProfile.IsDisabled = true;

                this.profileService.UpdateProfile(personProfile);
            }

            if (isDeleted)
            {
                return person.PersonId;
            }

            return 0;
        }
    }
}