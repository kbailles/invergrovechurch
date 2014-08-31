using System.Web.Security;

namespace InverGrove.Domain.Interfaces
{
    public interface IMembershipService
    {
        /// <summary>
        /// Gets the name of the membership by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        IMembership GetMembershipByUserName(string userName);

        /// <summary>
        /// Gets the membership by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IMembership GetMembershipByUserId(int userId);

        /// <summary>
        /// Creates the membership user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="passwordQuestion">The password question.</param>
        /// <param name="passwordAnswer">The password answer.</param>
        /// <param name="isApproved">if set to <c>true</c> [is approved].</param>
        /// <param name="passwordFormat">The password format.</param>
        /// <returns></returns>
        IMembership CreateMembershipUser(string userName, string password, string emailAddress, string passwordQuestion,
            string passwordAnswer, bool isApproved, MembershipPasswordFormat passwordFormat);

        /// <summary>
        /// Updates the membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <returns></returns>
        bool UpdateMembership(IMembership membership);
    }
}