using System.Web.Security;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Interfaces;
using MembershipProvider = InverGrove.Domain.Providers.MembershipProvider;

namespace InverGrove.Domain.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IMembershipService membershipService;
        private readonly IPersonService personService;
        private readonly IProfileService profileService;
        private const string DefaultPasswordQuestion = "Question";
        private const string DefaultPasswordAnswer = "Answer";
        private const int GeneratedPasswordLength = 128;
        private const int NumberNonAlphaNumericCharacters = 1;

        public RegistrationService(IMembershipService membershipService, IPersonService personService, IProfileService profileService)
        {
            this.membershipService = membershipService;
            this.personService = personService;
            this.profileService = profileService;
        }

        /// <summary>
        /// Registers the user.
        /// </summary>
        /// <param name="userToRegister">The user to register.</param>
        /// <returns></returns>
        /// <exception cref="ParameterNullException">userToRegister</exception>
        public bool RegisterUser(IRegister userToRegister)
        {
            if (ReferenceEquals(null, userToRegister))
            {
                throw new ParameterNullException("userToRegister");
            }

            if (ReferenceEquals(null, userToRegister.Person))
            {
                throw new ParameterNullException("userToRegister.Person");
            }

            int newProfileId = 0;

            string generatedPassword = MembershipProvider.GeneratePassword(GeneratedPasswordLength, NumberNonAlphaNumericCharacters);

            var newMembership = this.membershipService.CreateMembershipUser(userToRegister.UserName, generatedPassword,
                userToRegister.Person.PrimaryEmail, DefaultPasswordQuestion, DefaultPasswordAnswer,
                false, MembershipPasswordFormat.Hashed);

            if ((newMembership.MembershipId > 0) && (newMembership.UserId > 0))
            {
                var newPersonId = this.personService.AddPerson(userToRegister.Person);

                if (newPersonId > 0) // todo: consider rolling back entire transaction if any part fails.
                {
                    newProfileId = this.profileService.AddProfile(newMembership.UserId, newPersonId,
                    userToRegister.IsBaptized, userToRegister.IsLocal, userToRegister.IsActive, true);
                }
            }

            return newProfileId > 0;
        }
    }
}