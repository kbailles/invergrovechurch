using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Web.Profile;

namespace InverGrove.Domain.Interfaces
{
    public interface IProfileProvider
    {
        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        /// <exception cref="System.ArgumentNullException">config</exception>
        void Initialize(string name, NameValueCollection config);

        /// <summary>
        ///     Returns the collection of settings property values for the specified application instance and settings property group.
        /// </summary>
        /// <param name = "context">A <see cref = "T:System.Configuration.SettingsContext" /> describing the current application use.</param>
        /// <param name = "collection">A <see cref = "T:System.Configuration.SettingsPropertyCollection" /> containing the settings property group whose values are to be retrieved.</param>
        /// <returns>
        ///     A <see cref = "T:System.Configuration.SettingsPropertyValueCollection" /> containing the values for the specified settings property group.
        /// </returns>
        SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection);

        /// <summary>
        ///     Sets the values of the specified group of property settings.
        /// </summary>
        /// <param name = "context">A <see cref = "T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
        /// <param name = "collection">A <see cref = "T:System.Configuration.SettingsPropertyValueCollection" /> representing the group of property settings to set.</param>
        void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection);

        /// <summary>
        ///     Gets or sets the name of the currently running application.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     A <see cref = "T:System.String" /> that contains the application's shortened name, which does not contain a full path or extension, for example, SimpleAppSettings.
        /// </returns>
        string ApplicationName { get; set; }

        /// <summary>
        /// Gets the friendly name used to refer to the provider during configuration.
        /// </summary>
        /// <returns>The friendly name used to refer to the provider during configuration.</returns>
        string Name { get; }

        string Description { get; }

        /// <summary>
        ///     When overridden in a derived class, deletes profile properties and information for the supplied list of profiles.
        /// </summary>
        /// <param name = "profiles">A <see cref = "T:System.Web.Profile.ProfileInfoCollection" />  of information about profiles that are to be deleted.</param>
        /// <returns>
        ///     The number of profiles deleted from the data source.
        /// </returns>
        int DeleteProfiles(ProfileInfoCollection profiles);

        /// <summary>
        ///     When overridden in a derived class, deletes profile properties and information for profiles that match the supplied list of user names.
        /// </summary>
        /// <param name = "usernames">A string array of user names for profiles to be deleted.</param>
        /// <returns>
        ///     The number of profiles deleted from the data source.
        /// </returns>
        int DeleteProfiles(string[] usernames);

        /// <summary>
        ///     When overridden in a derived class, deletes all user-profile data for profiles in which the last activity date occurred before the specified date.
        /// </summary>
        /// <param name = "authenticationOption">One of the <see cref = "T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are deleted.</param>
        /// <param name = "userInactiveSinceDate">A <see cref = "T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see
        ///      cref = "P:System.Web.Profile.ProfileInfo.LastActivityDate" />  value of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
        /// <returns>
        ///     The number of profiles deleted from the data source.
        /// </returns>
        int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate);

        /// <summary>
        ///     When overridden in a derived class, returns the number of profiles in which the last activity date occurred on or before the specified date.
        /// </summary>
        /// <param name = "authenticationOption">One of the <see cref = "T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
        /// <param name = "userInactiveSinceDate">A <see cref = "T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see
        ///      cref = "P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
        /// <returns>
        ///     The number of profiles in which the last activity date occurred on or before the specified date.
        /// </returns>
        int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate);

        /// <summary>
        ///     When overridden in a derived class, retrieves user profile data for all profiles in the data source.
        /// </summary>
        /// <param name = "authenticationOption">One of the <see cref = "T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
        /// <param name = "pageIndex">The index of the page of results to return.</param>
        /// <param name = "pageSize">The size of the page of results to return.</param>
        /// <param name = "totalRecords">When this method returns, contains the total number of profiles.</param>
        /// <returns>
        ///     A <see cref = "T:System.Web.Profile.ProfileInfoCollection" /> containing user-profile information for all profiles in the data source.
        /// </returns>
        ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords);

        /// <summary>
        ///     When overridden in a derived class, retrieves user-profile data from the data source for profiles in which the last activity date occurred on or before the specified date.
        /// </summary>
        /// <param name = "authenticationOption">One of the <see cref = "T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
        /// <param name = "userInactiveSinceDate">A <see cref = "T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see
        ///      cref = "P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
        /// <param name = "pageIndex">The index of the page of results to return.</param>
        /// <param name = "pageSize">The size of the page of results to return.</param>
        /// <param name = "totalRecords">When this method returns, contains the total number of profiles.</param>
        /// <returns>
        ///     A <see cref = "T:System.Web.Profile.ProfileInfoCollection" /> containing user-profile information about the inactive profiles.
        /// </returns>
        ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords);

        /// <summary>
        ///     When overridden in a derived class, retrieves profile information for profiles in which the user name matches the specified user names.
        /// </summary>
        /// <param name = "authenticationOption">One of the <see cref = "T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
        /// <param name = "usernameToMatch">The user name to search for.</param>
        /// <param name = "pageIndex">The index of the page of results to return.</param>
        /// <param name = "pageSize">The size of the page of results to return.</param>
        /// <param name = "totalRecords">When this method returns, contains the total number of profiles.</param>
        /// <returns>
        ///     A <see cref = "T:System.Web.Profile.ProfileInfoCollection" /> containing user-profile information for profiles where the user name matches the supplied <paramref
        ///      name = "usernameToMatch" /> parameter.
        /// </returns>
        ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords);

        /// <summary>
        ///     When overridden in a derived class, retrieves profile information for profiles in which the last activity date occurred on or before the specified date and the user name matches the specified user name.
        /// </summary>
        /// <param name = "authenticationOption">One of the <see cref = "T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
        /// <param name = "usernameToMatch">The user name to search for.</param>
        /// <param name = "userInactiveSinceDate">A <see cref = "T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see
        ///      cref = "P:System.Web.Profile.ProfileInfo.LastActivityDate" /> value of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
        /// <param name = "pageIndex">The index of the page of results to return.</param>
        /// <param name = "pageSize">The size of the page of results to return.</param>
        /// <param name = "totalRecords">When this method returns, contains the total number of profiles.</param>
        /// <returns>
        ///     A <see cref = "T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for inactive profiles where the user name matches the supplied <paramref
        ///      name = "usernameToMatch" /> parameter.
        /// </returns>
        ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords);
    }
}