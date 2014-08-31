namespace InverGrove.Domain.Interfaces
{
    public interface IMembershipRepository
    {
        /// <summary>
        /// Gets the membership.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">userId</exception>
        IMembership GetMembership(int userId);

        /// <summary>
        /// Gets the name of the membership by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        IMembership GetMembershipByUserName(string userName);

        /// <summary>
        /// Updates the membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <returns></returns>
        IMembership UpdateMembership(IMembership membership);

        /// <summary>
        /// Creates the membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <returns></returns>
        IMembership CreateMembership(IMembership membership);
    }
}