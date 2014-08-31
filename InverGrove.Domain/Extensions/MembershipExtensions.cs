using System;
using System.Web.Security;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Extensions
{
    public static class MembershipExtensions
    {
        private const string InverGroveMembershipProviderName = "InverGroveMembershipProvider";

        /// <summary>
        /// To the membership user.
        /// </summary>
        /// <param name="membership">The account.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public static MembershipUser ToMembershipUser(this IMembership membership, string userName)
        {
            if (membership == null)
            {
                return null;
            }

            DateTime dateLastLogin = membership.DateLastLogin.HasValue
                ? membership.DateLastLogin.Value : DateTime.MinValue;

            DateTime dateLastLockedOut = membership.DateLockedOut.HasValue
                ? membership.DateLockedOut.Value : DateTime.MinValue;

            DateTime lastActivityDate = membership.DateLastActivity.HasValue ? membership.DateLastActivity.Value : DateTime.Now;

            return new MembershipUser(InverGroveMembershipProviderName, userName, membership.UserId,
                string.Empty, membership.PasswordQuestion, string.Empty, membership.IsApproved, membership.IsLockedOut,
                membership.DateCreated, dateLastLogin, lastActivityDate, membership.DateModified,
                dateLastLockedOut);
        }
    }
}