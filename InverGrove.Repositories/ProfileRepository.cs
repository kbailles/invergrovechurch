using System;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using InverGrove.Data;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Models;
using InverGrove.Domain.Utils;
using InverGrove.Repositories.Extensions;
using Invergrove.Domain.Models;

namespace InverGrove.Repositories
{
    public class ProfileRepository : Repository<Profile>
    {
        private readonly object syncRoot = new object();

        /// <summary>
        /// Gets the profile by user id.
        /// </summary>
        /// <param name="id">The user id.</param>
        /// <param name="name">The user name.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">userId is out of range in ProfileRepository.GetProfileByUserId</exception>
        /// <exception cref="System.ArgumentNullException">name</exception>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to get profile for user:  + userId +  with message:  + sql.Message</exception>
        public override Profile Get(int? id = null, string name = null)
        {
            if ((id != null) && (id <= 0))
            {
                throw new ArgumentException("userId is out of range in ProfileRepository.GetProfileByUserId");
            }

            if ((id == null) && (string.IsNullOrEmpty(name)))
            {
                throw new ArgumentNullException("name");
            }
            
            Profile profile = ObjectFactory.Create<Profile>();

            using (var db = (InverGroveContext)contextFactory.GetObjectContext())
            {
                try
                {
                    Data.Entities.Profile profileEntity;

                    if ((id != null) && (id > 0))
                    {
                        profileEntity = db.Profiles.FirstOrDefault(p => p.UserId == (int) id);
                    }
                    else
                    {
                        profileEntity = db.Profiles.FirstOrDefault(p => p.User.UserName == name);
                    }

                    if (profileEntity != null)
                    {
                        profile = profileEntity.ToModel();
                    }
                }
                catch (SqlException sql)
                {
                    throw new ApplicationException("Error occurred in attempting to get profile for user: " + id + " with message: " + sql.Message);
                }
            }

            return profile;
        }

        /// <summary>
        /// Creates the profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">profile</exception>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to create User with message:  + sql.Message</exception>
        public override Profile Add(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException("profile");
            }

            var currentDate = DateTime.Now;
            profile.DateCreated = currentDate;
            profile.DateModified = currentDate;
            
            Data.Entities.Profile profileEntity = profile.ToEntity();
            profileEntity.User = null;
            profileEntity.Person = null; // will need to add a person here...

            using (var db = (InverGroveContext)contextFactory.GetObjectContext())
            {
                db.Profiles.Add(profileEntity);

                using (var scope = new TransactionScope())
                {
                    try
                    {
                        db.SaveChanges();

                        scope.Complete();
                    }
                    catch (SqlException sql)
                    {
                        throw new ApplicationException("Error occurred in attempting to create User with message: " + sql.Message);
                    }
                }
            }

            profile.ProfileId = profileEntity.ProfileId;
            
            return profile;
        }

        /// <summary>
        /// Updates the profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">profile</exception>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to create User with message:  + sql.Message</exception>
        public override Profile Update(Profile profile)
        {
            if (profile == null)
            {
                throw new ArgumentNullException("profile");
            }

            using (var db = (InverGroveContext)contextFactory.GetObjectContext())
            {
                using (TimedLock.Lock(this.syncRoot))
                {
                    var profileEntity = db.Profiles.FirstOrDefault(p => p.ProfileId == profile.ProfileId);

                    if (profileEntity != null)
                    {
                        profileEntity.DateModified = DateTime.Now;
                        profileEntity.IsActive = profile.IsActive;
                        profileEntity.IsBaptized = profile.IsBaptized;
                        profileEntity.IsDisabled = profile.IsDisabled;
                        profileEntity.IsLocal = profile.IsLocal;
                        profileEntity.IsValidated = profile.IsValidated;
                        profileEntity.PersonId = profile.ProfileId;
                        profileEntity.UserId = profile.UserId;
                    }
                    using (var scope = new TransactionScope())
                    {
                        try
                        {
                            db.SaveChanges();

                            scope.Complete();
                        }
                        catch (SqlException sql)
                        {
                            profile.ErrorMessage = "Error occurred in attempting to update profile with message: " + sql.Message;
                            throw new ApplicationException("Error occurred in attempting to update profile with message: " + sql.Message);
                        }
                    }
                }
            }

            return profile;
        }
    }
}