using System;
using System.Web.Profile;

namespace InverGrove.Domain.Extensions
{
    public static class ProfileExtensions
    {
        /// <summary>
        /// Gets the profile value.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <param name="propertyValueKey">The property value key.</param>
        /// <returns></returns>
        public static string GetProfileValue(this ProfileBase profile, string propertyValueKey)
        {
            if (profile == null)
            {
                return string.Empty;
            }

            var profileValue = profile.GetPropertyValue(propertyValueKey);

            return profileValue != null ? profileValue.ToString() : string.Empty;
        }

        /// <summary>
        /// Users the identifier.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns></returns>
        public static int UserId(this ProfileBase profile)
        {
            if (profile == null)
            {
                return 0;
            }

            var userId = profile.GetPropertyValue("UserId");

            return userId != null ? Convert.ToInt32(userId) : 0;
        }
    }
}