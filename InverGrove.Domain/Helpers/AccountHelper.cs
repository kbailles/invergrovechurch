using InverGrove.Domain.Enums;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using Membership = InverGrove.Domain.Models.Membership;

namespace InverGrove.Domain.Helpers
{
    public static class AccountHelper
    {
        /// <summary>
        /// Validates the account password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="membership">The membership.</param>
        /// <returns></returns>
        public static bool ValidateMembershipPassword(string password, IMembership membership)
        {
            if (membership == null)
            {
                return false;
            }

            var maskedPassword = password.FormatPasscode((InverGrovePasswordFormat)membership.PasswordFormatId, membership.PasswordSalt);

            return (membership.Password == maskedPassword);
        }
    }
}