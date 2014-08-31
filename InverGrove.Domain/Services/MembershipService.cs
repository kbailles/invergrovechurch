using System;
using System.Web.Security;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using Invergrove.Domain.Interfaces;
using Invergrove.Domain.Models;

namespace InverGrove.Domain.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipFactory membershipFactory;
        private readonly IUserService userService;
        private readonly IRepository<Models.Membership> membershipRepository;

        public MembershipService(IUserService userService = null, IMembershipFactory membershipFactory = null, IRepository<Models.Membership> membershipRepository = null)
        {
            this.userService = userService ?? UserService.Create();
            this.membershipFactory = membershipFactory ?? MembershipFactory.Create();
            this.membershipRepository = membershipRepository ?? Repository<Models.Membership>.Create();
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static IMembershipService Create()
        {
            return new MembershipService();
        }

        /// <summary>
        /// Gets the name of the membership by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">userName</exception>
        public IMembership GetMembershipByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            return this.membershipRepository.Get(name: userName);
        }

        /// <summary>
        /// Gets the membership by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">userId is not valid in MembershipService.GetMembershipByUserId</exception>
        public IMembership GetMembershipByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("userId is not valid in MembershipService.GetMembershipByUserId");
            }

            return this.membershipRepository.Get(userId);
        }

        /// <summary>
        /// Creates the membership user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="passwordQuestion">The password question.</param>
        /// <param name="passwordAnswer">The password answer.</param>
        /// <param name="isApproved">if set to <c>true</c> [is approved].</param>
        /// <param name="passwordFormat">The password format.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">
        /// userName
        /// or
        /// password
        /// or
        /// passwordQuestion
        /// or
        /// passwordAnswer
        /// </exception>
        public IMembership CreateMembershipUser(string userName, string password, string emailAddress, string passwordQuestion,
                                            string passwordAnswer, bool isApproved, MembershipPasswordFormat passwordFormat)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            if (string.IsNullOrEmpty(passwordQuestion))
            {
                throw new ArgumentNullException("passwordQuestion");
            }

            if (string.IsNullOrEmpty(passwordAnswer))
            {
                throw new ArgumentNullException("passwordAnswer");
            }

            IMembership membership = null;

            var user = this.userService.CreateUser(userName);

            if (user != null)
            {
                var newMembership = this.membershipFactory.Create(user.UserId, password, isApproved, passwordQuestion, passwordAnswer, passwordFormat);
                membership = this.membershipRepository.Add((Models.Membership)newMembership);
            }

            return membership;
        }

        /// <summary>
        /// Updates the membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">membership</exception>
        public bool UpdateMembership(IMembership membership)
        {
            if (membership == null)
            {
                throw new ArgumentNullException("membership");
            }

            bool success = true;

            try
            {
                var result = this.membershipRepository.Update((Models.Membership)membership);

                if (!string.IsNullOrEmpty(result.ErrorMessage))
                {
                    success = false;
                }
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }
        
    }
}