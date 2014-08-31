using System.Web.Security;
using InverGrove.Domain.Enums;

namespace InverGrove.Domain.Extensions
{
    public static class PasswordFormatExtensions
    {
        /// <summary>
        /// To the inver grove password format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static InverGrovePasswordFormat ToInverGrovePasswordFormat(this MembershipPasswordFormat format)
        {
            switch (format)
            {
                case MembershipPasswordFormat.Clear:
                    return InverGrovePasswordFormat.Clear;
                case MembershipPasswordFormat.Hashed:
                    return InverGrovePasswordFormat.Hashed;
                case MembershipPasswordFormat.Encrypted:
                    return InverGrovePasswordFormat.Encrypted;
                default:
                    return InverGrovePasswordFormat.Clear;
            }
        }

        /// <summary>
        /// To the membership password format.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <returns></returns>
        public static MembershipPasswordFormat ToMembershipPasswordFormat(this InverGrovePasswordFormat format)
        {
            switch (format)
            {
                case InverGrovePasswordFormat.Clear:
                    return MembershipPasswordFormat.Clear;
                case InverGrovePasswordFormat.Hashed:
                    return MembershipPasswordFormat.Hashed;
                case InverGrovePasswordFormat.Encrypted:
                    return MembershipPasswordFormat.Encrypted;
                default:
                    return MembershipPasswordFormat.Clear;
            }
        }

        /// <summary>
        /// To the inver grove password format.
        /// </summary>
        /// <param name="membershipPasswordFormatId">The membership password format identifier.</param>
        /// <returns></returns>
        public static InverGrovePasswordFormat ToInverGrovePasswordFormat(this int membershipPasswordFormatId)
        {
            MembershipPasswordFormat passwordFormat = (MembershipPasswordFormat) membershipPasswordFormatId;

            return passwordFormat.ToInverGrovePasswordFormat();
        }

        /// <summary>
        /// To the membership password format.
        /// </summary>
        /// <param name="inverGrovePasswordFormatId">The inver grove password format identifier.</param>
        /// <returns></returns>
        public static MembershipPasswordFormat ToMembershipPasswordFormat(this int inverGrovePasswordFormatId)
        {
            InverGrovePasswordFormat passwordFormat = (InverGrovePasswordFormat)inverGrovePasswordFormatId;

            return passwordFormat.ToMembershipPasswordFormat();
        }
    }
}