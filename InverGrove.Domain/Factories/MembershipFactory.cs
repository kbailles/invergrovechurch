using System;
using System.Web.Security;
using InverGrove.Domain.Enums;
using InverGrove.Domain.Exceptions;
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
                throw new ParameterNullException("password");
            }

            if (string.IsNullOrEmpty(passwordQuestion))
            {
                throw new ParameterNullException("passwordQuestion");
            }

            if (string.IsNullOrEmpty(passwordAnswer))
            {
                throw new ParameterNullException("passwordAnswer");
            }

            var inverGrovePasswordFormat = passwordFormat.ToInverGrovePasswordFormat();

            var membership = new Membership
                             {
                                 PasswordSalt = password.GetRandomSalt(),
                                 PasswordFormatId = (int)inverGrovePasswordFormat,
                                 IsApproved = isApproved,
                                 PasswordQuestion = passwordQuestion
                             };

            var strippedSecurityAnswer = passwordAnswer.ToSecurityAnswer();
            var hashedSecurityAnswer = strippedSecurityAnswer.FormatPasscode(inverGrovePasswordFormat, membership.PasswordSalt);

            membership.Password = password.FormatPasscode(inverGrovePasswordFormat, membership.PasswordSalt);
            membership.PasswordAnswer = (inverGrovePasswordFormat == InverGrovePasswordFormat.Hashed) ? hashedSecurityAnswer : strippedSecurityAnswer;
            membership.FailedPasswordAttemptWindowStart = DateTime.MinValue.IsSqlSafeDate();
            membership.FailedPasswordAnswerAttemptWindowStart = DateTime.MinValue.IsSqlSafeDate();

            return membership;
        }
    }
}