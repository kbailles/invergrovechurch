namespace InverGrove.Domain.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Creates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">user</exception>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to create User with message:  + sql.Message</exception>
        IUser CreateUser(IUser user);

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to get User with message:  + sql.Message</exception>
        IUser GetUser(int userId);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IUser UpdateUser(IUser user);
    }
}