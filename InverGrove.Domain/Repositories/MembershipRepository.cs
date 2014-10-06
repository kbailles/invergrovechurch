using System;
using InverGrove.Data;
using InverGrove.Data.Entities;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Repositories
{
    public class MembershipRepository : EntityRepository<Membership, int>, IMembershipRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public MembershipRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public new static IMembershipRepository Create()
        {
            return new MembershipRepository(InverGroveContext.Create());
        }

        /// <summary>
        /// Adds the specified membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        /// <exception cref="ParameterNullException">profile</exception>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">profile</exception>
        public IMembership Add(IMembership membership, string userName)
        {
            if (membership == null)
            {
                throw new ParameterNullException("profile");
            }

            var timestamp = DateTime.Now;
            Membership membershipEntity = ((Models.Membership)membership).ToEntity();

            membershipEntity.FailedPasswordAnswerAttemptCount = 0;
            membershipEntity.FailedPasswordAttemptCount = 0;
            membershipEntity.IsLockedOut = false;
            membershipEntity.DateCreated = timestamp;
            membershipEntity.DateModified = timestamp;
            membershipEntity.User = ObjectFactory.Create<User>();
            membershipEntity.User.UserName = userName;
            membershipEntity.User.DateCreated = timestamp;
            membershipEntity.User.DateModified = timestamp;
            membershipEntity.User.LastActivityDate = timestamp;
            membershipEntity.User.IsAnonymous = false;
            membershipEntity.DateLockedOut = null;
            membershipEntity.DateLastActivity = timestamp;
            membershipEntity.DateLastLogin = timestamp;
            membershipEntity.IsApproved = true;
            membershipEntity.FailedPasswordAnswerAttemptWindowStart = timestamp;
            membershipEntity.FailedPasswordAttemptWindowStart = timestamp;

            this.Insert(membershipEntity);

            this.Save();
            
            return membershipEntity.ToModel();
        }

        /// <summary>
        /// Updates the specified membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">membership</exception>
        public void Update(IMembership membership)
        {
            if (membership == null)
            {
                throw new ParameterNullException("membership");
            }

            var membershipEntity = this.GetById(membership.MembershipId);

            if (membershipEntity != null)
            {
                membershipEntity.DateLastLogin = membership.DateLastLogin;
                membershipEntity.DateLockedOut = membership.DateLockedOut;
                membershipEntity.DateModified = DateTime.Now;
                membershipEntity.FailedPasswordAnswerAttemptCount = membership.FailedPasswordAnswerAttemptCount;
                membershipEntity.FailedPasswordAnswerAttemptWindowStart = membership.FailedPasswordAnswerAttemptWindowStart;
                membershipEntity.FailedPasswordAttemptCount = membership.FailedPasswordAttemptCount;
                membershipEntity.FailedPasswordAttemptWindowStart = membership.FailedPasswordAttemptWindowStart;
                membershipEntity.IsLockedOut = membership.IsLockedOut;
                membershipEntity.IsApproved = membership.IsApproved;
                membershipEntity.Password = membership.Password;
                membershipEntity.PasswordAnswer = membership.PasswordAnswer;
                membershipEntity.PasswordFormatId = membership.PasswordFormatId;
                membershipEntity.PasswordQuestion = membership.PasswordQuestion;
                membershipEntity.PasswordSalt = membership.PasswordSalt;
                membershipEntity.DateLastActivity = DateTime.Now;
            }

            // Don't cascade update (don't attempt to update user)
            this.dataContext.AutoDetectChanges = false;
            this.Update(membershipEntity);

            this.Save();
        }
    }
}