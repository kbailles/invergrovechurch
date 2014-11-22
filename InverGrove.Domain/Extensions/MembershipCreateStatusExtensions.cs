using System.Collections.Generic;
using System.Linq;
using System.Web.Security;
using InverGrove.Domain.Resources;

namespace InverGrove.Domain.Extensions
{
    public static class MembershipCreateStatusExtensions
    {
        private static readonly IDictionary<MembershipCreateStatus, string> membershipCreateStatusMessageDictionary =
            new Dictionary<MembershipCreateStatus, string>
            {
                {MembershipCreateStatus.DuplicateUserName, Messages.UsernameAlreadyExists},
                {MembershipCreateStatus.DuplicateEmail, Messages.DuplicateUserEmail},
                {MembershipCreateStatus.InvalidPassword, Messages.InvalidPassword},
                {MembershipCreateStatus.InvalidEmail, Messages.InvalidEmail},
                {MembershipCreateStatus.InvalidAnswer, Messages.InvalidPasswordAnswer},
                {MembershipCreateStatus.InvalidQuestion, Messages.InvalidPasswordQuestion},
                {MembershipCreateStatus.InvalidUserName, Messages.InvalidUserName},
                {MembershipCreateStatus.ProviderError, Messages.CreateMemberProviderError},
                {MembershipCreateStatus.UserRejected, Messages.CreateMemberUserRejected}
            };

        /// <summary>
        /// Errors the code to string.
        /// </summary>
        /// <param name="createStatus">The create status.</param>
        /// <returns></returns>
        public static string ErrorCodeToString(this MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            var createStatusMessage = membershipCreateStatusMessageDictionary.FirstOrDefault(m => m.Key == createStatus);

            return !string.IsNullOrEmpty(createStatusMessage.Value) ? createStatusMessage.Value : Messages.CreateMemberUnknownErorr;
        }
    }
}