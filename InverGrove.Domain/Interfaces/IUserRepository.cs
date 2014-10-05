namespace InverGrove.Domain.Interfaces
{
    public interface IUserRepository : IEntityRepository<Data.Entities.User, int>
    {
        /// <summary>
        /// Adds the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">user</exception>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to create User with message:  + ex.Message</exception>
        int Add(IUser user);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns></returns>
        IUser Update(IUser user);
    }
}