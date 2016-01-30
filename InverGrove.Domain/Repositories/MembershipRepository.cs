using System;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Text;
using InverGrove.Data;
using InverGrove.Data.Entities;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Services;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Repositories
{
    public class MembershipRepository : EntityRepository<Membership, int>, IMembershipRepository
    {
        private readonly ILogService logService;
        private readonly object syncRoot = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository" /> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="logService">The log service.</param>
        public MembershipRepository(IInverGroveContext dataContext)//, ILogService logService
            : base(dataContext)
        {
            this.logService = null; // logService;
        }

        /// <summary>
        /// Creates this instance.  This is here for initializing MembershipProvider as it's created before Ioc
        /// </summary>
        /// <returns></returns>
        public new static IMembershipRepository Create()
        {
            return new MembershipRepository(InverGroveContext.Create()); //, new LogService("", false)
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
                throw new ParameterNullException("membership");
            }

            Membership membershipEntity = this.GetNewMembershipEntity(membership, userName);

            this.Insert(membershipEntity);

            using (TimedLock.Lock(this.syncRoot))
            {
                try
                {
                    this.Save();
                }
                catch (SqlException sql)
                {
                    if (this.logService != null)
                    {
                        this.logService.WriteToErrorLog(
                            "Error occurred when attempting to insert a membership record with user name: " + userName +
                            " with message: " + sql.Message);
                    }

                    return membership;
                }
                catch (DbEntityValidationException dbe)
                {
                    var sb = new StringBuilder();
                    foreach (var error in dbe.EntityValidationErrors)
                    {
                        foreach (var ve in error.ValidationErrors)
                        {
                            sb.Append(ve.ErrorMessage + ", ");
                        }
                    }

                    if (this.logService != null)
                    {
                        this.logService.WriteToErrorLog(
                            "Error occurred when attempting to insert a membership record with user name: " + userName +
                            " with message: " + sb);
                    }

                    return membership;
                }
                catch (Exception ex)
                {
                    if (this.logService != null)
                    {
                        this.logService.WriteToErrorLog(
                            "Error occurred when attempting to insert a membership record with user name: " + userName +
                            " with message: " + ex.Message);
                    }

                    return membership;
                }
            }

            return membershipEntity.ToModel();
        }

        /// <summary>
        /// Updates the specified membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">membership</exception>
        public bool Update(IMembership membership)
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

            using (TimedLock.Lock(this.syncRoot))
            {
                try
                {
                    this.Save();
                }
                catch (SqlException sql)
                {
                    if (this.logService != null)
                    {
                        this.logService.WriteToErrorLog(
                            "Error occurred when attempting to update a membership record with MembershipId: " +
                            membership.MembershipId +
                            " with message: " + sql.Message);
                    }

                    return false;
                }
                catch (DbEntityValidationException dbe)
                {
                    var sb = new StringBuilder();
                    foreach (var error in dbe.EntityValidationErrors)
                    {
                        foreach (var ve in error.ValidationErrors)
                        {
                            sb.Append(ve.ErrorMessage + ", ");
                        }
                    }

                    if (this.logService != null)
                    {
                        this.logService.WriteToErrorLog(
                            "Error occurred when attempting to insert a membership record with with MembershipId: " +
                            membership.MembershipId +
                            " with message: " + sb);
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    if (this.logService != null)
                    {
                        this.logService.WriteToErrorLog(
                            "Error occurred when attempting to update a membership record with MembershipId: " +
                            membership.MembershipId +
                            " with message: " + ex.Message);
                    }

                    return false;
                }
            }

            return true;
        }

        private Membership GetNewMembershipEntity(IMembership membership, string userName)
        {
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
            membershipEntity.FailedPasswordAnswerAttemptWindowStart = timestamp;
            membershipEntity.FailedPasswordAttemptWindowStart = timestamp;

            return membershipEntity;
        }
    }
}