namespace InverGrove.Domain.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">userName</exception>
        IUser CreateUser(string userName);

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentOutOfRangeException">userId</exception>
        IUser GetUser(int userId);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        bool UpdateUser(IUser user);
    }
}