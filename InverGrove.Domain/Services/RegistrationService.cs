using System.Collections.Generic;
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
        private readonly IMembershipService membershipService;
        private readonly IProfileService profileService;
        private readonly IRoleRepository roleRepository;
        private readonly IUserRoleRepository userRoleRepository;
        private const string DefaultPasswordQuestion = "Question";
        private const string DefaultPasswordAnswer = "Answer";
        private const int GeneratedPasswordLength = 128;
        private const int NumberNonAlphaNumericCharacters = 1;
        private const string DefaultMemberRole = "Member";

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationService" /> class.
        /// </summary>
        /// <param name="membershipService">The membership service.</param>
        /// <param name="profileService">The profile service.</param>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="userRoleRepository">The user role repository.</param>
        public RegistrationService(IMembershipService membershipService, IProfileService profileService, 
            IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            this.membershipService = membershipService;
            this.profileService = profileService;
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
            var roles = this.roleRepository.Get();

            var roleSelectList = new List<SelectListItem>();            

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

            register.Roles = roleSelectList;

            return register;
        }


        /// <summary>
        /// Registers the user.
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

            var newMembership = this.membershipService.CreateMembershipUser(userToRegister.UserName, userToRegister.Password,
                userToRegister.UserEmail, DefaultPasswordQuestion, DefaultPasswordAnswer,
                false, MembershipPasswordFormat.Hashed);

            if ((newMembership.MembershipId > 0) && (newMembership.UserId > 0))
            {
                registerUserResult.Success = this.profileService.AddProfile(newMembership.UserId, userToRegister.PersonId, 
                    true, true) > 0;

                var roleId = userToRegister.RoleId <= 0 ? 
                    this.roleRepository.Get(x => x.Description == DefaultMemberRole).First().RoleId : userToRegister.RoleId;

                this.userRoleRepository.AddUserToRole(newMembership.UserId, roleId);

                // Update to hashed password
                userToRegister.Password = newMembership.Password;               
            }

            return registerUserResult;
        }
    }
}