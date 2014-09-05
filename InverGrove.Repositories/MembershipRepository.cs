using System;
using System.Data.SqlClient;
using System.Transactions;
using InverGrove.Data;
using InverGrove.Domain.Models;
using InverGrove.Domain.Utils;
using InverGrove.Repositories.Extensions;
using InverGrove.Repositories.Queries;
using Invergrove.Domain.Models;

namespace InverGrove.Repositories
{
    public class MembershipRepository : Repository<Membership>
    {
        private readonly object syncRoot = new object();

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <param name="name">The user name.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">userId</exception>
        public override Membership Get(int? id = null, string name = null)
        {
            if ((id != null) && (id <= 0))
            {
                throw new ArgumentException("id");
            }

            if((id == null) && (string.IsNullOrEmpty(name)))
            {
                throw new ArgumentNullException("name");
            }

            Membership membership;

            using (var db = (InverGroveContext) contextFactory.GetObjectContext())
            {
                if ((id != null) && (id > 0))
                {
                    membership = MembershipQueries.GetMembershopByUserId(db, (int)id);
                }
                else
                {
                    membership = MembershipQueries.GetMembershipByUserName(db, name);
                }
            }

            return membership;
        }
       
        /// <summary>
        /// Creates the membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <returns></returns>
        public override Membership Add(Membership membership)
        {
            if (membership == null)
            {
                throw new ArgumentNullException("membership");
            }

            var timestamp = DateTime.Now;
            Data.Entities.Membership membershipEntity = membership.ToEntity();

            membershipEntity.FailedPasswordAnswerAttemptCount = 0;
            membershipEntity.FailedPasswordAttemptCount = 0;
            membershipEntity.IsLockedOut = false;
            membershipEntity.DateCreated = timestamp;
            membershipEntity.DateModified = timestamp;
            membershipEntity.User = null;

            using (var db = (InverGroveContext)contextFactory.GetObjectContext())
            {
                db.Memberships.Add(membershipEntity);
                
                using (TimedLock.Lock(this.syncRoot))
                {
                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            db.SaveChanges();

                            scope.Complete();
                        }
                        catch (SqlException sql)
                        {
                            throw new ApplicationException("Error occurred in attempting to create Membership with message: " + sql.Message);
                        }
                    }
                }
            }

            membership.MembershipId = membershipEntity.MembershipId;

            return membership;
        }

        /// <summary>
        /// Updates the membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <returns></returns>
        public override Membership Update(Membership membership)
        {
            if (membership == null)
            {
                throw new ArgumentNullException("membership");
            }

            using (var db = (InverGroveContext)contextFactory.GetObjectContext())
            {
                Data.Entities.Membership membershipEntity = MembershipQueries.GetMembershipEntityByUserId(db, membership.UserId);

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
                    membershipEntity.User = null;
                }

                using (TimedLock.Lock(this.syncRoot))
                {
                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            db.SaveChanges();

                            scope.Complete();
                        }
                        catch (SqlException sql)
                        {
                            throw new ApplicationException(
                                "Error occurred in attempting to update the Membership information with message: " + sql.Message +
                                " for: " + membership.UserId);
                        }
                    }
                }
            }

            return membership;
        }
    }
}