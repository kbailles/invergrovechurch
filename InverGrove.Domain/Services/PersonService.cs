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
            // Todo: these could be cached.
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
        public IPerson AddPerson(IPerson person, string hostName)
        {
            Guard.ParameterNotNull(person, "person");

            /* TODO - Check if this EMAIL address exists.  Email will be the sole check to guard against duplicate persons.
               TODO - People will be a cached list that is placed into cache when a person logs into the website.
                      This list will never be more than a couple hundred (likley around 150) so no big deal caching that. 
            */

            if (person.IsUser)
            {
                var existingEmail = this.EmailExists(person);

                if (existingEmail.Item2)
                {
                    person.PersonId = existingEmail.Item1;
                    person.ErrorMessage = "This email address already exists.";

                    return person;
                }
            }

            person.PersonId = this.personRepository.Add(person);

            if (person.IsUser && (person.PersonId > 0))
            {
                this.SendNewUserVerification(person, hostName);
            }

            return person;
        }

        /// <summary>
        /// Edits the specified person.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="hostName">Name of the host.</param>
        /// <returns></returns>
        public IPerson Edit(IPerson person, string hostName = "")
        {
            Guard.ParameterNotNull(person, "person");

            if (person.IsUser && this.EmailExists(person).Item2)
            {
                person.ErrorMessage = "This email address already exists.";

                return person;
            }

            var existingPerson = this.GetById(person.PersonId);
            var updatedPerson = this.personRepository.Update(person);

            if (string.IsNullOrEmpty(updatedPerson.ErrorMessage))
            {
                if (person.IsUser && !existingPerson.IsUser)
                {
                    this.SendNewUserVerification(person, hostName);
                }
            }

            return person;
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

        private Tuple<int, bool> EmailExists(IPerson person)
        {
            var existingPerson = this.personRepository.Get(p => p.EmailPrimary == person.PrimaryEmail).FirstOrDefault();
            var emailExists = existingPerson != null && existingPerson.PersonId > 0;

            return new Tuple<int, bool>(existingPerson.PersonId, emailExists);
        }

        private void SendNewUserVerification(IPerson person, string hostName)
        {
            var acccessToken = this.verificationRepository.Add(person.PersonId);

            // Send email notification
            this.emailService.SendNewUserEmail(person, acccessToken, hostName);
        }
    }
}