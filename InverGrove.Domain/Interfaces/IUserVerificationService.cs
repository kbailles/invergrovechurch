using System;

namespace InverGrove.Domain.Interfaces
{
    public interface IUserVerificationService
    {
        /// <summary>
        /// Adds the user invite notice.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        bool AddUserInviteNotice(int personId);

        /// <summary>
        /// Gets the user invite notice.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns></returns>
        IUserVerification GetUserInviteNotice(Guid identifier);

        /// <summary>
        /// Updates the user invite notice.
        /// </summary>
        /// <param name="userVerification">The user verification.</param>
        /// <returns></returns>
        bool UpdateUserInviteNotice(IUserVerification userVerification)
    }
}