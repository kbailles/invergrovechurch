using System;

namespace InverGrove.Domain.Interfaces
{
    /// <summary>
    /// Creates a notification record in the db for people being added to church directory
    /// who will also be website users.
    /// </summary>
    public interface IUserVerificationRepository
    {
        /// <summary>
        /// Adds the specified person identifier.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        Guid Add(int personId);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns></returns>
        IUserVerification Get(Guid identifier);

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="userVerification">The user verification.</param>
        /// <returns></returns>
        bool Update(IUserVerification userVerification);
    }
}