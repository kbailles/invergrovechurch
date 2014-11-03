using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Repositories;

namespace InverGrove.Domain.Services
{
    public class MembershipService : IMembershipService
    {
        private readonly IMembershipFactory membershipFactory;
        private readonly IMembershipRepository membershipRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipService" /> class.
        /// </summary>
        /// <param name="membershipFactory">The membership factory.</param>
        /// <param name="membershipRepository">The membership repository.</param>
        public MembershipService(IMembershipFactory membershipFactory = null, IMembershipRepository membershipRepository = null)
        {
            this.membershipFactory = membershipFactory ?? MembershipFactory.Create();
            this.membershipRepository = membershipRepository ?? MembershipRepository.Create();
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

            var foundMember = this.membershipRepository.Get(m => m.User.UserName == userName).FirstOrDefault();

            if (foundMember != null)
            {
                return foundMember.ToModel();
            }

            return ObjectFactory.Create<Models.Membership>();
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

            var foundMember = this.membershipRepository.Get(m => m.UserId == userId).FirstOrDefault();

            if (foundMember != null)
            {
                return foundMember.ToModel();
            }

            return ObjectFactory.Create<Models.Membership>();
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

            IMembership membership = this.membershipFactory.Create(password, isApproved, passwordQuestion, passwordAnswer, passwordFormat);
            var newMembership = this.membershipRepository.Add(membership, userName);

            return newMembership;
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
                this.membershipRepository.Update(membership);
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        /// <summary>
        /// Gets all membership users.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMembership> GetAllMembershipUsers()
        {
            var membershipUsers = this.membershipRepository.Get(includeProperties: "User");
            var membershipUserList = new List<IMembership>();

            foreach (var membership in membershipUsers)
            {
                membershipUserList.Add(membership.ToModel());
            }

            return membershipUserList;
        }
        
    }
}