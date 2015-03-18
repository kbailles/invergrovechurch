﻿using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using InverGrove.Domain.Enums;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.ViewModels;
using MembershipProvider = InverGrove.Domain.Providers.MembershipProvider;

namespace InverGrove.Domain.Services
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IEmailService emailService;
        private readonly IMembershipService membershipService;
        private readonly IProfileService profileService;
        private readonly IMaritalStatusRepository maritalStatusRepository;
        private readonly IChurchRoleRepository churchRoleRepository;
        private readonly IRoleRepository roleRepository;
        private readonly IUserRoleRepository userRoleRepository;
        private const string DefaultPasswordQuestion = "Question";
        private const string DefaultPasswordAnswer = "Answer";
        private const int GeneratedPasswordLength = 128;
        private const int NumberNonAlphaNumericCharacters = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationService" /> class.
        /// </summary>
        /// <param name="emailService">The email service.</param>
        /// <param name="membershipService">The membership service.</param>
        /// <param name="profileService">The profile service.</param>
        /// <param name="maritalStatusRepository">The marital status repository.</param>
        /// <param name="churchRoleRepository">The church role repository.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="userRoleRepository">The user role repository.</param>
        public RegistrationService(IEmailService emailService, IMembershipService membershipService, IProfileService profileService, IMaritalStatusRepository maritalStatusRepository,
            IChurchRoleRepository churchRoleRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            this.emailService = emailService;
            this.membershipService = membershipService;
            this.profileService = profileService;
            this.maritalStatusRepository = maritalStatusRepository;
            this.churchRoleRepository = churchRoleRepository;
            this.roleRepository = roleRepository;
            this.userRoleRepository = userRoleRepository;
        }

        /// <summary>
        /// Gets the register view model.
        /// </summary>
        /// <param name="hasSiteAdminRole">if set to <c>true</c> [has site admin role].</param>
        /// <returns></returns>
        public IRegister GetRegisterViewModel(bool hasSiteAdminRole)
        {
            var register = ObjectFactory.Create<Register>();
            var maritalStatusList = this.maritalStatusRepository.Get();
            var churchRoles = this.churchRoleRepository.Get();
            var roles = this.roleRepository.Get();

            var churchRoleSelectList = new List<SelectListItem>();
            var maritalSelectList = new List<SelectListItem>();
            var roleSelectList = new List<SelectListItem>();

            foreach (var maritalStatus in maritalStatusList)
            {
                maritalSelectList.Add(new SelectListItem
                                      {
                                          Text = maritalStatus.MaritalStatusDescription,
                                          Value = maritalStatus.MaritalStatusId.ToString(CultureInfo.InvariantCulture)
                                      });
            }

            foreach (var churchRole in churchRoles)
            {
                churchRoleSelectList.Add(new SelectListItem
                {
                    Text = churchRole.ChurchRoleDescription,
                    Value = churchRole.ChurchRoleId.ToString(CultureInfo.InvariantCulture)
                });
            }

            foreach (var role in roles)
            {
                roleSelectList.Add(new SelectListItem
                {
                    Text = role.Description,
                    Value = role.RoleId.ToString(CultureInfo.InvariantCulture),
                    Selected = role.Description == RoleType.Member.ToString()
                });
            }

            // Don't allow lower level user to add someone to the SiteAdmin role.
            if (!hasSiteAdminRole)
            {
                var siteAdminRole = roleSelectList.Find(r => r.Text == RoleType.SiteAdmin.ToString());
                roleSelectList.Remove(siteAdminRole);
            }

            register.MaritalStatusList = maritalSelectList;
            register.ChurchRoleList = churchRoleSelectList;
            register.Roles = roleSelectList;

            return register;
        }

        /// <summary>
        /// TODO - Method signature to contain only PersonID
        /// </summary>
        /// <param name="userToRegister">The user to register.</param>
        /// <returns></returns>
        /// <exception cref="ParameterNullException">userToRegister</exception>
        public IRegisterUserResult RegisterUser(IRegister userToRegister)
        {
            if (ReferenceEquals(null, userToRegister))
            {
                throw new ParameterNullException("userToRegister");
            }


            var registerUserResult = RegisterUserResult.Create();

            var existingUserName = this.userRoleRepository.Get(u => u.User.UserName == userToRegister.UserName).FirstOrDefault();

            if (existingUserName != null)
            {
                registerUserResult.MembershipCreateStatus = MembershipCreateStatus.DuplicateUserName;

                return registerUserResult;
            }

            string generatedPassword = MembershipProvider.GeneratePassword(GeneratedPasswordLength, NumberNonAlphaNumericCharacters);

            var newMembership = this.membershipService.CreateMembershipUser(userToRegister.UserName, generatedPassword,
                userToRegister.Person.PrimaryEmail, DefaultPasswordQuestion, DefaultPasswordAnswer,
                false, MembershipPasswordFormat.Hashed);

            if ((newMembership.MembershipId > 0) && (newMembership.UserId > 0))
            {
                // commented out LB 3/17/2015
                //registerUserResult.Success = this.profileService.AddPersonProfile(userToRegister.Person, newMembership.UserId,
                //    userToRegister.IsLocal, userToRegister.IsActive, true);

                //this.userRoleRepository.AddUserToRole(newMembership.UserId, userToRegister.RoleId);

                // Update to hashed password
                userToRegister.Password = newMembership.Password;

                this.emailService.SendNewUserEmail(userToRegister);
            }

            return registerUserResult;
        }
    }
}