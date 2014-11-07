using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.ViewModels;
using MembershipProvider = InverGrove.Domain.Providers.MembershipProvider;

namespace InverGrove.Domain.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IMembershipService membershipService;
        private readonly IProfileService profileService;
        private readonly IMaritalStatusRepository maritalStatusRepository;
        private readonly IPersonTypeRepository personTypeRepository;
        private const string DefaultPasswordQuestion = "Question";
        private const string DefaultPasswordAnswer = "Answer";
        private const int GeneratedPasswordLength = 128;
        private const int NumberNonAlphaNumericCharacters = 1;

        public RegistrationService(IMembershipService membershipService,  IProfileService profileService, IMaritalStatusRepository maritalStatusRepository,
            IPersonTypeRepository personTypeRepository)
        {
            this.membershipService = membershipService;
            this.profileService = profileService;
            this.maritalStatusRepository = maritalStatusRepository;
            this.personTypeRepository = personTypeRepository;
        }

        /// <summary>
        /// Gets the register view model.
        /// </summary>
        /// <returns></returns>
        public IRegister GetRegisterViewModel()
        {
            var register = ObjectFactory.Create<Register>();
            var maritalStatusList = this.maritalStatusRepository.Get();
            var personTypes = this.personTypeRepository.Get();

            var personTypeSelectList = new List<SelectListItem>();
            var maritalSelectList = new List<SelectListItem>();

            foreach (var maritalStatus in maritalStatusList)
            {
                maritalSelectList.Add(new SelectListItem
                                      {
                                          Text = maritalStatus.MaritalStatusDescription,
                                          Value = maritalStatus.MaritalStatusId.ToString(CultureInfo.InvariantCulture)
                                      });
            }

            foreach (var personType in personTypes)
            {
                personTypeSelectList.Add(new SelectListItem
                {
                    Text = personType.PersonTypeDescription,
                    Value = personType.PersonTypeId.ToString(CultureInfo.InvariantCulture)
                });
            }

            register.MaritalStatusList = maritalSelectList;
            register.PersonTypeList = personTypeSelectList;

            return register;
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

            bool success = false;

            string generatedPassword = MembershipProvider.GeneratePassword(GeneratedPasswordLength, NumberNonAlphaNumericCharacters);

            var newMembership = this.membershipService.CreateMembershipUser(userToRegister.UserName, generatedPassword,
                userToRegister.Person.PrimaryEmail, DefaultPasswordQuestion, DefaultPasswordAnswer,
                false, MembershipPasswordFormat.Hashed);

            if ((newMembership.MembershipId > 0) && (newMembership.UserId > 0))
            {
                success = this.profileService.AddPersonProfile(userToRegister.Person, newMembership.UserId,
                    userToRegister.IsBaptized, userToRegister.IsLocal, userToRegister.IsActive, true);
            }

            return success;
        }
    }
}