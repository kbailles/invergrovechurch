using System.Configuration;

namespace InverGrove.Domain.Interfaces
{
    public interface IProfileFactory
    {
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
        SettingsPropertyValueCollection BuildSettingsPropertyCollection(IProfile profile,
                                                                                        SettingsPropertyValueCollection spvc);
    }
}