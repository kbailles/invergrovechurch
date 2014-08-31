using System;
using InverGrove.Domain.Models;

namespace InverGrove.Repositories.Extensions
{
    public static class EntityExtensions
    {
        public static Membership ToModel(this Data.Entities.Membership membership)
        {
            if (membership == null)
            {
                return null;
            }

            return new Membership
                       {
                           MembershipId = membership.MembershipId,
                           DateLastLogin = membership.DateLastLogin,
                           DateLockedOut = membership.DateLockedOut,
                           DateModified = DateTime.Now,
                           FailedPasswordAnswerAttemptCount = membership.FailedPasswordAnswerAttemptCount,
                           FailedPasswordAnswerAttemptWindowStart = membership.FailedPasswordAnswerAttemptWindowStart,
                           FailedPasswordAttemptCount = membership.FailedPasswordAttemptCount,
                           FailedPasswordAttemptWindowStart = membership.FailedPasswordAttemptWindowStart,
                           IsLockedOut = membership.IsLockedOut,
                           IsApproved = membership.IsApproved,
                           Password = membership.Password,
                           PasswordAnswer = membership.PasswordAnswer,
                           PasswordFormatId = membership.PasswordFormat,
                           PasswordQuestion = membership.PasswordQuestion,
                           PasswordSalt = membership.PasswordSalt,
                           DateLastActivity = membership.DateLastActivity
                       };
        }

        public static Data.Entities.Membership ToEntity(this Membership membership)
        {
            if (membership == null)
            {
                return null;
            }

            return new Data.Entities.Membership
            {
                MembershipId = membership.MembershipId,
                DateLastLogin = membership.DateLastLogin,
                DateLockedOut = membership.DateLockedOut,
                DateModified = DateTime.Now,
                FailedPasswordAnswerAttemptCount = membership.FailedPasswordAnswerAttemptCount,
                FailedPasswordAnswerAttemptWindowStart = membership.FailedPasswordAnswerAttemptWindowStart,
                FailedPasswordAttemptCount = membership.FailedPasswordAttemptCount,
                FailedPasswordAttemptWindowStart = membership.FailedPasswordAttemptWindowStart,
                IsLockedOut = membership.IsLockedOut,
                IsApproved = membership.IsApproved,
                Password = membership.Password,
                PasswordAnswer = membership.PasswordAnswer,
                PasswordFormat = (byte)membership.PasswordFormatId,
                PasswordQuestion = membership.PasswordQuestion,
                PasswordSalt = membership.PasswordSalt,
                DateLastActivity = membership.DateLastActivity
            };
        }

        public static Data.Entities.User ToEntity(this User user)
        {
            if (user == null)
            {
                return null;
            }

            return new Data.Entities.User
                   {
                       UserId = user.UserId,
                       UserName = user.UserName,
                       DateCreated = user.DateCreated,
                       DateModified = user.DateModified,
                       LastActivityDate = user.LastActivityDate,
                       IsAnonymous = user.IsAnonymous
                   };
        }

        public static User ToModel(this Data.Entities.User user)
        {
            if (user == null)
            {
                return null;
            }

            return new User
            {
                UserId = user.UserId,
                UserName = user.UserName,
                DateCreated = user.DateCreated,
                DateModified = user.DateModified,
                LastActivityDate = user.LastActivityDate,
                IsAnonymous = user.IsAnonymous
            };
        }

        public static Data.Entities.Profile ToEntity(this Profile profile)
        {
            if (profile == null)
            {
                return null;
            }

            return new Data.Entities.Profile
                   {
                       ProfileId = profile.ProfileId,
                       PersonId = profile.PersonId,
                       ReceiveEmailNotification = profile.ReceiveEmailNotification,
                       IsActive = profile.IsActive,
                       IsBaptized = profile.IsBaptized,
                       IsDisabled = profile.IsDisabled,
                       IsLocal = profile.IsLocal,
                       IsValidated = profile.IsValidated,
                       UserId = profile.UserId,
                       DateModified = profile.DateModified,
                       DateCreated = profile.DateCreated
                   };
        }

        public static Profile ToModel(this Data.Entities.Profile profile)
        {
            if (profile == null)
            {
                return null;
            }

            return new Profile
            {
                ProfileId = profile.ProfileId,
                PersonId = profile.PersonId,
                ReceiveEmailNotification = profile.ReceiveEmailNotification,
                IsActive = profile.IsActive,
                IsBaptized = profile.IsBaptized,
                IsDisabled = profile.IsDisabled,
                IsLocal = profile.IsLocal,
                IsValidated = profile.IsValidated,
                UserId = profile.UserId,
                DateModified = profile.DateModified,
                DateCreated = profile.DateCreated
            };
        }
    }
}