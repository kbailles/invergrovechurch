using System.Web.Security;

namespace InverGrove.Domain.Interfaces
{
    public interface IMembershipFactory
    {
        /// <summary>
        /// Creates the specified user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="password">The password.</param>
        /// <param name="isApproved">if set to <c>true</c> [is approved].</param>
        /// <param name="passwordQuestion">The password question.</param>
        /// <param name="passwordAnswer">The password answer.</param>
        /// <param name="passwordFormat">The password format.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">password</exception>
        IMembership Create(int userId, string password, bool isApproved, string passwordQuestion, string passwordAnswer, MembershipPasswordFormat passwordFormat);
    }
}