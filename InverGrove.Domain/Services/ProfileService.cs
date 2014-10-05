using System;
using System.Configuration;
using System.Linq;
using System.Web.Profile;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Repositories;
using InverGrove.Domain.ValueTypes;

namespace InverGrove.Domain.Services
{
    public class ProfileService : IProfileService
    {
        //private readonly ILogService logService;
        private readonly IProfileProvider profileProvider;
        private readonly ISessionStateService sessionStateService;
        private readonly IProfileRepository profileRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileService" /> class.
        /// </summary>
        /// <param name="profileProvider">The profile provider.</param>
        /// <param name="sessionStateService">The session state service.</param>
        /// <param name="profileRepository">The profile repository.</param>
        public ProfileService(IProfileProvider profileProvider, ISessionStateService sessionStateService, IProfileRepository profileRepository = null)
        {
            this.profileProvider = profileProvider;
            //this.logService = logService;
            this.sessionStateService = sessionStateService;
            this.profileRepository = profileRepository ?? ProfileRepository.Create();
        }


        /// <summary>
        /// Gets the property values.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="isAuthenticated">if set to <c>true</c> [is authenticated].</param>
        /// <returns></returns>
        public SettingsPropertyValueCollection GetPropertyValues(string username, bool isAuthenticated = true)
        {
            return this.profileProvider.GetPropertyValues(this.CreateSettingsContext(username, isAuthenticated), ProfileBase.Properties);
        }

        /// <summary>
        /// Sets the property values.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="isAuthenticated">if set to <c>true</c> [is authenticated].</param>
        /// <param name="collection">The collection.</param>
        public void SetPropertyValues(string username, SettingsPropertyValueCollection collection, bool isAuthenticated = true)
        {
            this.profileProvider.SetPropertyValues(this.CreateSettingsContext(username, isAuthenticated), collection);
        }

        /// <summary>
        /// Removes the profile data from session.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="userName">Name of the user.</param>
        public void RemoveProfileDataFromSession(int userId, string userName)
        {
            //clear profile data by user-name
            this.sessionStateService.Remove(string.Format(CacheKey.UserProfileKey, userName));

            //clear profile data by user id
            this.sessionStateService.Remove(string.Format(CacheKey.UserProfileIDKey, userId));
        }

        /// <summary>
        /// Gets the profile by user id.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns></returns>
        public IProfile GetProfile(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("userId is zero in ProfileService.GetProfile");
            }

            string cacheKey = string.Format(CacheKey.UserProfileIDKey, userId);

            return this.sessionStateService.TryGet(cacheKey, userId, "", this.GetRepositoryProfile);
        }

        /// <summary>
        /// Gets the profile by the user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public IProfile GetProfileByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("userName");
            }

            string cacheKey = string.Format(CacheKey.UserProfileKey, userName);

            return this.sessionStateService.TryGet(cacheKey, 0, userName, this.GetRepositoryProfile);
        }

        private SettingsContext CreateSettingsContext(string username, bool isAuthenticated)
        {
            var settingsContext = new SettingsContext
			{
				{
					"UserName", username
				},
				{
					"IsAuthenticated", isAuthenticated
				}
			};
            return settingsContext;
        }

        internal IProfile GetRepositoryProfile(int userId, string userName)
        {
            IProfile profile = Profile.Create();

            if ((userId <= 0) && (string.IsNullOrEmpty(userName)))
            {
                return profile;
            }

            if (userId > 0)
            {
                var foundProfile = this.profileRepository.Get(p => p.UserId == userId).FirstOrDefault();

                if (foundProfile != null)
                {
                    profile = foundProfile.ToModel();
                }
            }

            if (!string.IsNullOrEmpty(userName))
            {
                var foundProfile = this.profileRepository.Get(p => p.User.UserName == userName).FirstOrDefault();

                if (foundProfile != null)
                {
                    profile = foundProfile.ToModel();
                }
            }

            return profile;
        }
    }
}