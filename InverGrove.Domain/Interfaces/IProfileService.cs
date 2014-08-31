using System.Configuration;

namespace InverGrove.Domain.Interfaces
{
    public interface IProfileService
    {
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