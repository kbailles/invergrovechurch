using System;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Text;
using System.Web.Hosting;
using InverGrove.Data;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Services;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Repositories
{
    public class ProfileRepository : EntityRepository<Data.Entities.Profile, int>, IProfileRepository
    {
        private readonly ILogService logService;
        private readonly object syncRoot = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileRepository" /> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        /// <param name="logService">The log service.</param>
        public ProfileRepository(IInverGroveContext dataContext, ILogService logService)
            : base(dataContext)
        {
            this.logService = logService;
        }

        /// <summary>
        /// Creates this instance.  This is here just for the provider as it gets created before the Ioc.
        /// </summary>
        /// <returns></returns>
        public new static IProfileRepository Create()
        {
            return new ProfileRepository(InverGroveContext.Create(), new LogService("", false));
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
            profileEntity.Person = null;
            profileEntity.User = null;

            this.Insert(profileEntity);

            using (TimedLock.Lock(this.syncRoot))
            {
                try
                {
                    this.Save();
                }
                catch (SqlException ex)
                {
                    this.logService.WriteToErrorLog(
                        "Error occurred when attempting to add a profile record with UserId: " + profile.UserId +
                        " with message: " + ex.Message);
                }
                catch (DbEntityValidationException dbe)
                {
                    var sb = new StringBuilder();
                    foreach (var error in dbe.EntityValidationErrors)
                    {
                        foreach (var ve in error.ValidationErrors)
                        {
                            sb.Append(ve.ErrorMessage + ", ");
                        }
                    }

                    this.logService.WriteToErrorLog("Error occurred when attempting to add a profile record with UserId: " + profile.UserId +
                    " with message: " + sb);
                }
            }

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
            profileEntity.Person = ((Person)person).ToEntity();
            profileEntity.User = null;

            this.Insert(profileEntity);

            using (TimedLock.Lock(this.syncRoot))
            {
                try
                {
                    this.Save();
                }
                catch (SqlException ex)
                {
                    this.logService.WriteToErrorLog(
                        "Error occurred when attempting to add a profile record with UserId: " + profile.UserId +
                        " with message: " + ex.Message);
                }
                catch (DbEntityValidationException dbe)
                {
                    var sb = new StringBuilder();
                    foreach (var error in dbe.EntityValidationErrors)
                    {
                        foreach (var ve in error.ValidationErrors)
                        {
                            sb.Append(ve.ErrorMessage + ", ");
                        }
                    }

                    this.logService.WriteToErrorLog("Error occurred when attempting to add a profile record with UserId: " + profile.UserId +
                    " with message: " + sb);
                }
            }

            return profileEntity.ProfileId;
        }

        /// <summary>
        /// Updates the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">profile</exception>
        public bool Update(IProfile profile)
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
                profileEntity.IsDisabled = profile.IsDisabled;
                profileEntity.IsValidated = profile.IsValidated;
                profileEntity.PersonId = profile.ProfileId;
                profileEntity.UserId = profile.UserId;
            }

            // Don't cascade update (don't attempt to update user/person)
            this.dataContext.AutoDetectChanges = false;
            this.Update(profileEntity);

            using (TimedLock.Lock(this.syncRoot))
            {
                try
                {
                    this.Save();
                }
                catch (SqlException sql)
                {
                    this.logService.WriteToErrorLog("Error occurred when attempting to update a profile record with ProfileId: " + profile.ProfileId +
                    " with message: " + sql.Message);

                    return false;
                }
                catch (DbEntityValidationException dbe)
                {
                    var sb = new StringBuilder();
                    foreach (var error in dbe.EntityValidationErrors)
                    {
                        foreach (var ve in error.ValidationErrors)
                        {
                            sb.Append(ve.ErrorMessage + ", ");
                        }
                    }

                    this.logService.WriteToErrorLog("Error occurred when attempting to update a profile record with ProfileId: " + profile.ProfileId +
                    " with message: " + sb);

                    return false;
                }
            }

            return true;
        }
    }
}