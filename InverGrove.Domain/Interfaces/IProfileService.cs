using System.Configuration;

namespace InverGrove.Domain.Interfaces
{
    public interface IProfileService
    {
        /// <summary>
        /// Adds the profile.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="personId">The person identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <param name="isValidated">if set to <c>true</c> [is validated].</param>
        /// <returns></returns>
        int AddProfile(int userId, int personId, bool isActive, bool isValidated);

        /// <summary>
        /// Adds the person profile.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="userId">The user identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <param name="isValidated">if set to <c>true</c> [is validated].</param>
        /// <returns></returns>
        bool AddPersonProfile(IPerson person, int userId, bool isActive, bool isValidated);

        /// <summary>
        /// Gets the property values.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="isAuthenticated">if set to <c>true</c> [is authenticated].</param>
        /// <returns></returns>
        SettingsPropertyValueCollection GetPropertyValues(string username, bool isAuthenticated = true);

        /// <summary>
        /// Sets the property values.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="isAuthenticated">if set to <c>true</c> [is authenticated].</param>
        /// <param name="collection">The collection.</param>
        void SetPropertyValues(string username, SettingsPropertyValueCollection collection, bool isAuthenticated = true);

        /// <summary>
        /// Removes the profile data from session.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="userName">Name of the user.</param>
        void RemoveProfileDataFromSession(int userId, string userName);

        /// <summary>
        /// Gets the profile by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        IProfile GetProfile(int userId);

        /// <summary>
        /// Gets the profile by the user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        IProfile GetProfileByUserName(string userName);
    }
}