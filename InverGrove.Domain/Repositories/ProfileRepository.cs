using System;
using InverGrove.Data;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;

namespace InverGrove.Domain.Repositories
{
    public class ProfileRepository : EntityRepository<Data.Entities.Profile, int>, IProfileRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public ProfileRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public new static IProfileRepository Create()
        {
            return new ProfileRepository(InverGroveContext.Create());
        }

        /// <summary>
        /// Adds the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">profile</exception>
        public int Add(IProfile profile)
        {
            if (profile == null)
            {
                throw new ParameterNullException("profile");
            }

            var currentDate = DateTime.Now;
            profile.DateCreated = currentDate;
            profile.DateModified = currentDate;

            Data.Entities.Profile profileEntity = ((Profile)profile).ToEntity();
            profileEntity.IsDisabled = false;
            profileEntity.IsValidated = true;
            profileEntity.Person = null;
            profileEntity.User = null;

            this.Insert(profileEntity);

            this.Save();

            return profileEntity.ProfileId;
        }

        /// <summary>
        /// Adds the person profile.
        /// </summary>
        /// <param name="person">The person.</param>
        /// <param name="profile">The profile.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">
        /// person
        /// or
        /// profile
        /// </exception>
        public int AddPersonProfile(IPerson person, IProfile profile)
        {
            if (person == null)
            {
                throw new ParameterNullException("person");
            }

            if (profile == null)
            {
                throw new ParameterNullException("profile");
            }

            var currentDate = DateTime.Now;
            profile.DateCreated = currentDate;
            profile.DateModified = currentDate;

            Data.Entities.Profile profileEntity = ((Profile)profile).ToEntity();
            profileEntity.IsDisabled = false;
            profileEntity.IsValidated = true;
            profileEntity.Person = ((Person)person).ToEntity();
            profileEntity.User = null;

            this.Insert(profileEntity);

            this.Save();

            return profileEntity.ProfileId;
        }

        /// <summary>
        /// Updates the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">profile</exception>
        public void Update(IProfile profile)
        {
            if (profile == null)
            {
                throw new ParameterNullException("profile");
            }

            var profileEntity = this.GetById(profile.ProfileId);

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

            this.Update(profileEntity);

            this.Save();
        }
    }
}