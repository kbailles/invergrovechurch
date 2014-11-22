using System.Web.Security;
using InverGrove.Domain.Models;

namespace InverGrove.Domain.Interfaces
{
    public interface IRegisterUserResult
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RegisterUserResult"/> is success.
        /// </summary>
        /// <value>
        ///   <c>true</c> if success; otherwise, <c>false</c>.
        /// </value>
        bool Success { get; set; }

        /// <summary>
        /// Gets or sets the membership create status.
        /// </summary>
        /// <value>
        /// The membership create status.
        /// </value>
        MembershipCreateStatus MembershipCreateStatus { get; set; }
    }
}