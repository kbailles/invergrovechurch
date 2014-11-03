using System;
using System.Web.Security;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using Membership = InverGrove.Domain.Models.Membership;

namespace InverGrove.Domain.Factories
{
    public class MembershipFactory : IMembershipFactory
    {
        private static readonly Lazy<MembershipFactory> instance = 
            new Lazy<MembershipFactory>(() => new MembershipFactory());

        public static IMembershipFactory Create()
        {
            return instance.Value;
        }

        /// <summary>
        /// Creates the specified user id.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="isApproved">if set to <c>true</c> [is approved].</param>
        /// <param name="passwordQuestion">The password question.</param>
        /// <param name="passwordAnswer">The password answer.</param>
        /// <param name="passwordFormat">The password format.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">password</exception>
        public IMembership Create(string password, bool isApproved,
            string passwordQuestion, string passwordAnswer, MembershipPasswordFormat passwordFormat)
        {
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

            var inverGrovePasswordFormat = passwordFormat.ToInverGrovePasswordFormat();

            var membership = new Membership
                             {
                                 PasswordSalt = password.GetRandomSalt(),
                                 PasswordFormatId = (int)inverGrovePasswordFormat,
                                 IsApproved = isApproved,
                                 PasswordQuestion = passwordQuestion
                             };

            membership.Password = password.FormatPasscode(inverGrovePasswordFormat, membership.PasswordSalt);
            membership.PasswordAnswer = passwordAnswer.ToLowerInvariant().Sha256Hash();
            membership.FailedPasswordAttemptWindowStart = DateTime.MinValue.IsSqlSafeDate();
            membership.FailedPasswordAnswerAttemptWindowStart = DateTime.MinValue.IsSqlSafeDate();

            return membership;
        }
    }
}