﻿using System;
using System.Collections.Generic;
using System.Linq;
using InverGrove.Domain.Models;

namespace InverGrove.Domain.Extensions
{
    public static class EntityExtensions
    {
        public static Membership ToModel(this Data.Entities.Membership membership)
        {
            var membershipModel = new Membership();

            if (membership == null)
            {
                return membershipModel;
            }

            membershipModel.MembershipId = membership.MembershipId;
            membershipModel.DateLastLogin = membership.DateLastLogin;
            membershipModel.DateLockedOut = membership.DateLockedOut;
            membershipModel.DateModified = membership.DateModified;
            membershipModel.FailedPasswordAnswerAttemptCount = membership.FailedPasswordAnswerAttemptCount;
            membershipModel.FailedPasswordAnswerAttemptWindowStart = membership.FailedPasswordAnswerAttemptWindowStart;
            membershipModel.FailedPasswordAttemptCount = membership.FailedPasswordAttemptCount;
            membershipModel.FailedPasswordAttemptWindowStart = membership.FailedPasswordAttemptWindowStart;
            membershipModel.IsLockedOut = membership.IsLockedOut;
            membershipModel.IsApproved = membership.IsApproved;
            membershipModel.Password = membership.Password;
            membershipModel.PasswordAnswer = membership.PasswordAnswer;
            membershipModel.PasswordFormatId = membership.PasswordFormatId;
            membershipModel.PasswordQuestion = membership.PasswordQuestion;
            membershipModel.PasswordSalt = membership.PasswordSalt;
            membershipModel.DateLastActivity = membership.DateLastActivity;
            membershipModel.UserId = membership.UserId;

            if (membership.User != null)
            {
                membershipModel.User = membership.User.ToModel();
            }

            return membershipModel;
        }

        public static Data.Entities.Membership ToEntity(this Membership membershipModel)
        {
            var membership = new Data.Entities.Membership();

            if (membershipModel == null)
            {
                return membership;
            }

            membership.MembershipId = membershipModel.MembershipId;
            membership.DateLastLogin = membershipModel.DateLastLogin;
            membership.DateLockedOut = membershipModel.DateLockedOut;
            membership.DateModified = DateTime.Now;
            membership.FailedPasswordAnswerAttemptCount = membershipModel.FailedPasswordAnswerAttemptCount;
            membership.FailedPasswordAnswerAttemptWindowStart = membershipModel.FailedPasswordAnswerAttemptWindowStart;
            membership.FailedPasswordAttemptCount = membershipModel.FailedPasswordAttemptCount;
            membership.FailedPasswordAttemptWindowStart = membershipModel.FailedPasswordAttemptWindowStart;
            membership.IsLockedOut = membershipModel.IsLockedOut;
            membership.IsApproved = membershipModel.IsApproved;
            membership.Password = membershipModel.Password;
            membership.PasswordAnswer = membershipModel.PasswordAnswer;
            membership.PasswordFormatId = membershipModel.PasswordFormatId;
            membership.PasswordQuestion = membershipModel.PasswordQuestion;
            membership.PasswordSalt = membershipModel.PasswordSalt;
            membership.DateLastActivity = membershipModel.DateLastActivity;

            if (membershipModel.User != null)
            {
                var user = (User)membershipModel.User;
                membership.User = user.ToEntity();
            }

            return membership;
        }

        public static Data.Entities.User ToEntity(this User userModel)
        {
            var user = new Data.Entities.User();

            if (userModel == null)
            {
                return user;
            }

            user.UserId = userModel.UserId;
            user.UserName = userModel.UserName;
            user.DateCreated = userModel.DateCreated;
            user.DateModified = userModel.DateModified;
            user.LastActivityDate = userModel.LastActivityDate;
            user.IsAnonymous = userModel.IsAnonymous;

            return user;
        }

        public static User ToModel(this Data.Entities.User user)
        {
            var userModel = new User();

            if (user == null)
            {
                return userModel;
            }

            userModel.UserId = user.UserId;
            userModel.UserName = user.UserName;
            userModel.DateCreated = user.DateCreated;
            userModel.DateModified = user.DateModified;
            userModel.LastActivityDate = user.LastActivityDate;
            userModel.IsAnonymous = user.IsAnonymous;

            return userModel;
        }

        public static Data.Entities.Profile ToEntity(this Profile profileModel)
        {
            var profile = new Data.Entities.Profile();

            if (profileModel == null)
            {
                return profile;
            }

            profile.ProfileId = profileModel.ProfileId;               ;
            profile.PersonId = profileModel.PersonId;
            profile.ReceiveEmailNotification = profileModel.ReceiveEmailNotification;
            profile.IsActive = profileModel.IsActive;
            profile.IsBaptized = profileModel.IsBaptized;
            profile.IsDisabled = profileModel.IsDisabled;
            profile.IsLocal = profileModel.IsLocal;
            profile.IsValidated = profileModel.IsValidated;
            profile.UserId = profileModel.UserId;
            profile.DateModified = profileModel.DateModified;
            profile.DateCreated = profileModel.DateCreated;

            return profile;
        }

        public static Profile ToModel(this Data.Entities.Profile profile)
        {
            var profileModel = new Profile();

            if (profile == null)
            {
                return profileModel;
            }

            profileModel.ProfileId = profile.ProfileId;
            profileModel.PersonId = profile.PersonId;
            profileModel.ReceiveEmailNotification = profile.ReceiveEmailNotification;
            profileModel.IsActive = profile.IsActive;
            profileModel.IsBaptized = profile.IsBaptized;
            profileModel.IsDisabled = profile.IsDisabled;
            profileModel.IsLocal = profile.IsLocal;
            profileModel.IsValidated = profile.IsValidated;
            profileModel.UserId = profile.UserId;
            profileModel.DateModified = profile.DateModified;
            profileModel.DateCreated = profile.DateCreated;

            return profileModel;
        }

        public static Role ToModel(this Data.Entities.Role entityRole)
        {
            var roleModel = new Role();

            if (entityRole == null)
            {
                return roleModel;
            }

            roleModel.RoleId = entityRole.RoleId;
            roleModel.Description = entityRole.Description;

            return roleModel;
        }

        public static IEnumerable<Role> ToModelCollection(this IEnumerable<Data.Entities.Role> entityRoles)
        {
            var roles = new List<Role>();

            if (entityRoles == null)
            {
                return roles;
            }

            roles.AddRange(entityRoles.Select(entityRole => entityRole.ToModel()));

            return roles;
        }
    }
}