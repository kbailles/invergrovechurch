namespace InverGrove.Domain.Interfaces
{
    public interface IProfileRepository
    {
        /// <summary>
        /// Gets the profile by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">userId is out of range in ProfileRepository.GetProfileByUserId</exception>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to get profile for user:  + userId +  with message:  + sql.Message</exception>
        IProfile GetProfileByUserId(int userId);

        /// <summary>
        /// Gets the name of the profile by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">userName</exception>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to get profile for user:  + userName +  with message:  + sql.Message</exception>
        IProfile GetProfileByUserName(string userName);

        /// <summary>
        /// Creates the profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">profile</exception>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to create User with message:  + sql.Message</exception>
        IProfile CreateProfile(IProfile profile);

        /// <summary>
        /// Updates the profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">profile</exception>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to create User with message:  + sql.Message</exception>
        IProfile UpdateProfile(IProfile profile);
    }
}