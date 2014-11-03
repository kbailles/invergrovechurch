using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Profile;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Repositories;
using InverGrove.Domain.ValueTypes;

namespace InverGrove.Domain.Providers
{
    public class ProfileProvider : System.Web.Profile.ProfileProvider, IProfileProvider
    {
        private readonly IProfileRepository profileRepository;
        private readonly IProfileFactory profileFactory;
        private string providerName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileProvider"/> class.
        /// </summary>
        public ProfileProvider()
            : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileProvider"/> class.
        /// </summary>
        /// <param name="profileFactory">The profile factory.</param>
        /// <param name="profileRepository">The profile repository.</param>
        public ProfileProvider(IProfileFactory profileFactory, IProfileRepository profileRepository)
        {
            this.profileFactory = profileFactory ?? ProfileFactory.Instance;
            this.profileRepository = profileRepository ?? ProfileRepository.Create();
        }

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific attributes specified in the configuration for this provider.</param>
        /// <exception cref="System.ArgumentNullException">config</exception>
        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ParameterNullException("config");
            }
            if (string.IsNullOrEmpty(name))
            {
                name = "InverGroveProfileProvider";
            }

            this.providerName = name;
            this.ApplicationName = "InverGrove";

            base.Initialize(name, config);
        }

        /// <summary>
        ///     Returns the collection of settings property values for the specified application instance and settings property group.
        /// </summary>
        /// <param name = "context">A <see cref = "T:System.Configuration.SettingsContext" /> describing the current application use.</param>
        /// <param name = "collection">A <see cref = "T:System.Configuration.SettingsPropertyCollection" /> containing the settings property group whose values are to be retrieved.</param>
        /// <returns>
        ///     A <see cref = "T:System.Configuration.SettingsPropertyValueCollection" /> containing the values for the specified settings property group.
        /// </returns>
        public override SettingsPropertyValueCollection GetPropertyValues(SettingsContext context, SettingsPropertyCollection collection)
        {
            if (context == null)
            {
                throw new ParameterNullException("context");
            }
            if (collection == null)
            {
                throw new ParameterNullException("collection");
            }

            SettingsPropertyValueCollection spvc = new SettingsPropertyValueCollection();

            if (collection.Count > 0)
            {
                this.AddSettingsPropertiesToCollection(collection, spvc);

                spvc = this.SetSettingsPropertyValueCollectionWithProfileVaues(context, spvc);
            }

            return spvc;
        }

        /// <summary>
        ///     Sets the values of the specified group of property settings.
        /// </summary>
        /// <param name = "context">A <see cref = "T:System.Configuration.SettingsContext" /> describing the current application usage.</param>
        /// <param name = "collection">A <see cref = "T:System.Configuration.SettingsPropertyValueCollection" /> representing the group of property settings to set.</param>
        public override void SetPropertyValues(SettingsContext context, SettingsPropertyValueCollection collection)
        {
            if (context == null)
            {
                throw new ParameterNullException("context");
            }

            if (collection == null)
            {
                throw new ParameterNullException("collection");
            }

            string userName = context["UserName"] as string;

            if (string.IsNullOrEmpty(userName))
            {
                // ReSharper disable once NotResolvedInText
                throw new ParameterNullException("context['UserName']");
            }

            var profileModel = this.profileFactory.Create(collection);

            this.SaveProfile((Profile)profileModel, userName);
        }

        /// <summary>
        ///     Gets or sets the name of the currently running application.
        /// </summary>
        /// <value></value>
        /// <returns>
        ///     A <see cref = "T:System.String" /> that contains the application's shortened name, which does not contain a full path or extension, for example, SimpleAppSettings.
        /// </returns>
        public override string ApplicationName { get; set; }

        /// <summary>
        /// Gets the friendly name used to refer to the provider during configuration.
        /// </summary>
        /// <returns>The friendly name used to refer to the provider during configuration.</returns>
        public override string Name
        {
            get
            {
                return this.providerName;
            }
        }

        /// <summary>
        ///     When overridden in a derived class, deletes profile properties and information for the supplied list of profiles.
        /// </summary>
        /// <param name = "profiles">A <see cref = "T:System.Web.Profile.ProfileInfoCollection" />  of information about profiles that are to be deleted.</param>
        /// <returns>
        ///     The number of profiles deleted from the data source.
        /// </returns>
        public override int DeleteProfiles(ProfileInfoCollection profiles)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     When overridden in a derived class, deletes profile properties and information for profiles that match the supplied list of user names.
        /// </summary>
        /// <param name = "usernames">A string array of user names for profiles to be deleted.</param>
        /// <returns>
        ///     The number of profiles deleted from the data source.
        /// </returns>
        public override int DeleteProfiles(string[] usernames)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     When overridden in a derived class, deletes all user-profile data for profiles in which the last activity date occurred before the specified date.
        /// </summary>
        /// <param name = "authenticationOption">One of the <see cref = "T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are deleted.</param>
        /// <param name = "userInactiveSinceDate">A <see cref = "T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see
        ///      cref = "P:System.Web.Profile.ProfileInfo.LastActivityDate" />  value of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
        /// <returns>
        ///     The number of profiles deleted from the data source.
        /// </returns>
        public override int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     When overridden in a derived class, returns the number of profiles in which the last activity date occurred on or before the specified date.
        /// </summary>
        /// <param name = "authenticationOption">One of the <see cref = "T:System.Web.Profile.ProfileAuthenticationOption" /> values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
        /// <param name = "userInactiveSinceDate">A <see cref = "T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see
        ///      cref = "P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
        /// <returns>
        ///     The number of profiles in which the last activity date occurred on or before the specified date.
        /// </returns>
        public override int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
        {
            throw new NotImplementedException();
        }

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
        public override ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

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
        public override ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

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
        public override ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

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
        public override ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        private void AddSettingsPropertiesToCollection(SettingsPropertyCollection collection,
            SettingsPropertyValueCollection spvc)
        {
            foreach (SettingsProperty property in collection)
            {
                if (property.SerializeAs == SettingsSerializeAs.ProviderSpecific)
                {
                    property.SerializeAs = ((property.PropertyType.IsPrimitive) || (property.PropertyType == typeof(string)))
                        ? SettingsSerializeAs.String
                        : SettingsSerializeAs.Xml;
                }

                spvc.Add(new SettingsPropertyValue(property));
            }
        }

        private SettingsPropertyValueCollection SetSettingsPropertyValueCollectionWithProfileVaues(SettingsContext context, SettingsPropertyValueCollection spvc)
        {
            var userName = context["UserName"] as string;

            if (!string.IsNullOrEmpty(userName))
            {
                string cacheKey = string.Format(CacheKey.UserProfileKey, userName);

                var profile = this.TryGet(cacheKey, userName, this.GetProfile);

                if (profile != null)
                {
                    spvc = this.profileFactory.BuildSettingsPropertyCollection(profile, spvc);
                }
            }

            return spvc;
        }

        private IProfile GetProfile(string userName)
        {
            var result = this.profileRepository.Get(p => p.User.UserName == userName).FirstOrDefault();

            if (result == null)
            {
                throw new ApplicationException("Error retrieving user profile");
            }

            return result.ToModel();
        }

        private void SaveProfile(Profile profile, string userName)
        {
            var savedProfileId = this.profileRepository.Add(profile);

            if (savedProfileId <= 0)
            {
                throw new ApplicationException("Error occurred when attempting to save the profile in ProfileProvider.SaveProfile");
            }

            profile.ProfileId = savedProfileId;

            //cached by user-name
            HttpContext.Current.Session[string.Format(CacheKey.UserProfileKey, userName)] = profile;
            //cached by  user id
            HttpContext.Current.Session[string.Format(CacheKey.UserProfileIDKey, profile.UserId)] = profile;
        }

        private TResult TryGet<T1, TResult>(string key, T1 t1, Func<T1, TResult> serviceFunc = null, bool cacheResult = true)
        {
            var result = HttpContext.Current.Session[key];

            if ((result == null) && (serviceFunc != null))
            {
                result = serviceFunc.Invoke(t1);

                if (result != null && cacheResult)
                {
                    HttpContext.Current.Session[key] = result;
                }
            }
            return (TResult)result;
        }
    }
}