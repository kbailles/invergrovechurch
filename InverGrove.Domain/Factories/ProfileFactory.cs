using System;
using System.Configuration;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;

namespace InverGrove.Domain.Factories
{
    public class ProfileFactory : IProfileFactory
    {
        private static readonly Lazy<ProfileFactory> instance =
            new Lazy<ProfileFactory>(() => new ProfileFactory());

        /// <summary>
        ///   Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public static IProfileFactory Instance
        {
            get { return instance.Value; }
        }


        /// <summary>
        /// Creates the specified user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="personId">The person identifier.</param>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        /// <param name="isValidated">if set to <c>true</c> [is validated].</param>
        /// <param name="isLocal">if set to <c>true</c> [is local].</param>
        /// <returns></returns>
        public IProfile Create(int userId, int personId,bool isActive, bool isValidated, bool isLocal=true)
        {
            return new Profile
            {
                IsActive = isActive,
                IsLocal = isLocal,
                IsValidated = isValidated,
                PersonId = personId,
                UserId = userId
            };
        }

        /// <summary>
        /// Creates the Profile from the specified collection.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <returns></returns>
        public IProfile Create(SettingsPropertyValueCollection collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }

            var profile = new Profile
                          {
                              ProfileId = (int) collection["ProfileId"].PropertyValue,
                              UserId = (int) collection["UserId"].PropertyValue,
                              ReceiveEmailNotification = (bool) collection["ReceiveEmailNotification"].PropertyValue,
                              PersonId = (int) collection["PersonId"].PropertyValue,
                              IsLocal = (bool) collection["IsLocal"].PropertyValue,
                              IsActive = (bool) collection["IsActive"].PropertyValue,
                              IsDisabled = (bool) collection["IsDisabled"].PropertyValue,
                              IsValidated = (bool) collection["IsValidated"].PropertyValue,
                              DateCreated = (DateTime) collection["DateCreated"].PropertyValue,
                              DateModified = (DateTime) collection["DateModified"].PropertyValue
                          };

            if (collection["Person"].PropertyValue != null)
            {
                profile.Person = (Person)collection["Person"].PropertyValue;
            }

            return profile;
        }

        /// <summary>
        /// Creates the settings collection.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <param name="spvc">The SPVC.</param>
        /// <returns></returns>
        public SettingsPropertyValueCollection BuildSettingsPropertyCollection(IProfile profile,
            SettingsPropertyValueCollection spvc)
        {
            if (spvc == null)
            {
                throw new ArgumentNullException("spvc");
            }

            if (profile != null)
            {
                spvc["ProfileId"].PropertyValue = profile.ProfileId;
                spvc["UserId"].PropertyValue = profile.UserId;
                spvc["ReceiveEmailNotification"].PropertyValue = profile.ReceiveEmailNotification;
                spvc["PersonId"].PropertyValue = profile.PersonId;
                spvc["IsLocal"].PropertyValue = profile.IsLocal;
                spvc["IsActive"].PropertyValue = profile.IsActive;
                spvc["IsDisabled"].PropertyValue = profile.IsDisabled;
                spvc["IsValidated"].PropertyValue = profile.IsValidated;
                spvc["DateCreated"].PropertyValue = profile.DateCreated;
                spvc["DateModified"].PropertyValue = profile.DateModified;
                spvc["Person"].PropertyValue = profile.Person;
            }

            return spvc;
        }
    }
}