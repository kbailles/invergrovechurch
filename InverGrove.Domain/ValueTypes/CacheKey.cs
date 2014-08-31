namespace InverGrove.Domain.ValueTypes
{
    public static class CacheKey
    {
        /// <summary>
        /// The cache key to query for the cached profile
        /// </summary>
        public const string UserProfileKey = "UserProfile.{0}";

        /// <summary>
        /// The cache key to query for the cached profile by ID
        /// </summary>
        public const string UserProfileIDKey = "UserProfileByID.{0}";
    }
}