using System.Configuration;

namespace InverGrove.Domain.Interfaces
{
    public interface IProfileFactory
    {
        /// <summary>
        /// Creates the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="personId">The person identifier.</param>
        /// <param name="isLocal">if set to <c>true</c> [is local].</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <param name="isValidated">if set to <c>true</c> [is validated].</param>
        /// <returns></returns>
        IProfile Create(int userId, int personId, bool isLocal, bool isActive, bool isValidated);

        /// <summary>
        /// Creates the Profile from the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        IProfile Create(SettingsPropertyValueCollection collection);

        /// <summary>
        /// Creates the settings collection.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <param name="spvc">The SPVC.</param>
        /// <returns></returns>
        SettingsPropertyValueCollection BuildSettingsPropertyCollection(IProfile profile, SettingsPropertyValueCollection spvc);
    }
}