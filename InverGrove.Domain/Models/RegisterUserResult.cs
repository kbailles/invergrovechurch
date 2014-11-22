using System.Web.Security;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Models
{
    public class RegisterUserResult : IRegisterUserResult
    {
        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public static IRegisterUserResult Create()
        {
            return new RegisterUserResult();
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RegisterUserResult"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the membership create status.
        /// </summary>
        /// <value>
        /// The membership create status.
        /// </value>
        public MembershipCreateStatus MembershipCreateStatus { get; set; }
    }
}