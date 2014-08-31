using System;
using System.Linq;
using InverGrove.Data;
using InverGrove.Domain.Models;

namespace InverGrove.Repositories.Queries
{
    public static class MembershipQueries
    {
        internal static readonly Func<InverGroveContext, int, Membership> GetMembershopByUserId =
            (db, userId) =>
                (from m in db.Memberships
                 where m.UserId == userId
                 select new Membership
                            {
                                MembershipId = m.MembershipId,
                                UserId = m.UserId,
                                DateLastLogin = m.DateLastLogin,
                                DateLockedOut = m.DateLockedOut,
                                FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
                                FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart,
                                FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
                                FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
                                IsLockedOut = m.IsLockedOut,
                                IsApproved = m.IsApproved,
                                Password = m.Password,
                                PasswordSalt = m.PasswordSalt,
                                PasswordFormatId = m.PasswordFormat,
                                PasswordQuestion = m.PasswordQuestion,
                                PasswordAnswer = m.PasswordAnswer,
                                DateCreated = m.DateCreated,
                                DateModified = m.DateModified
                            }).FirstOrDefault();

        internal static readonly Func<InverGroveContext, int, Data.Entities.Membership> GetMembershipEntityByUserId =
           (db, userId) => (from m in db.Memberships
                                 where m.UserId == userId
                                 select m).FirstOrDefault();

        internal static readonly Func<InverGroveContext, string, Membership> GetMembershipByUserName =
            (db, userName) => (from m in db.Memberships
                                    join u in db.Users on m.UserId equals u.UserId
                                    where u.UserName == userName
                                    select new Membership
                                    {
                                        MembershipId = m.MembershipId,
                                        UserId = m.UserId,
                                        DateLastLogin = m.DateLastLogin,
                                        DateLockedOut = m.DateLockedOut,
                                        FailedPasswordAnswerAttemptCount = m.FailedPasswordAnswerAttemptCount,
                                        FailedPasswordAnswerAttemptWindowStart = m.FailedPasswordAnswerAttemptWindowStart,
                                        FailedPasswordAttemptCount = m.FailedPasswordAttemptCount,
                                        FailedPasswordAttemptWindowStart = m.FailedPasswordAttemptWindowStart,
                                        IsLockedOut = m.IsLockedOut,
                                        IsApproved = m.IsApproved,
                                        Password = m.Password,
                                        PasswordSalt = m.PasswordSalt,
                                        PasswordFormatId = m.PasswordFormat,
                                        PasswordQuestion = m.PasswordQuestion,
                                        PasswordAnswer = m.PasswordAnswer,
                                        DateCreated = m.DateCreated,
                                        DateModified = m.DateModified
                                    }).FirstOrDefault();
    }
}