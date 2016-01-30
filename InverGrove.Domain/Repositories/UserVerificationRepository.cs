using System;
using System.Data.SqlClient;
using System.Linq;
using InverGrove.Data;
using InverGrove.Data.Entities;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Repositories
{
    public class UserVerificationRepository : EntityRepository<UserVerification, int>, IUserVerificationRepository
    {
        private readonly ILogService logService;

        public UserVerificationRepository(IInverGroveContext dataContext)//, ILogService logService
            : base(dataContext)
        {
            this.logService = null; // logService;
        }

        /// <summary>
        /// Adds the specified person identifier.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        public Guid Add(int personId)
        {
            Guard.ParameterNotGreaterThanZero(personId, "personId");

            var userVerification = new UserVerification
                                   {
                                       PersonId = personId,
                                       Identifier = Guid.NewGuid(),
                                       DateSent = DateTime.Now
                                   };

            this.Insert(userVerification);

            try
            {
                this.Save();
            }
            catch (SqlException ex)
            {
                if (this.logService != null)
                {
                    this.logService.WriteToErrorLog(
                        "Error when attempting to add new UserVerificatioin in UserVerificationRepository: " + ex.Message);
                }

                return Guid.Empty;
            }

            return userVerification.Identifier;
        }

        /// <summary>
        /// Gets the specified identifier UPON COMING TO SET UP THEIR UID / PWD
        /// ARE THEY VALID?
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns></returns>
        public IUserVerification Get(Guid identifier)
        {
            Guard.ParameterGuidNotEmpty(identifier, "identifier");

            var userVerification = (from uv in this.dataContext.UserVerifications
                      join pr in this.dataContext.People
                      on uv.PersonId equals pr.PersonId
                      where uv.Identifier.Equals(identifier) && uv.DateAccessed.Equals(null)
                      select new Models.UserVerification { PersonId = uv.PersonId,
                                                           Identifier = uv.Identifier,
                                                           DateSent = uv.DateSent,
                                                           DateAccessed = uv.DateAccessed,
                                                           PersonName = pr.FirstName + " " + pr.LastName
                                                        }).FirstOrDefault();
            return userVerification;
        }

        /// <summary>
        /// Updates the specified identifier.
        /// </summary>
        /// <param name="userVerification">The user verification.</param>
        /// <returns></returns>
        public bool Update(IUserVerification userVerification)
        {
            var userVerificationEntity = this.Get(x => x.Identifier == userVerification.Identifier).FirstOrDefault();

            if (userVerificationEntity != null)
            {
                userVerificationEntity.DateAccessed = userVerification.DateAccessed;

                try
                {
                    this.Save();
                }
                catch (SqlException ex)
                {
                    if (this.logService != null)
                    {
                        this.logService.WriteToErrorLog("Error when attempting to update UserVerificatioin in UserVerificationRepository with Identifier of: "
                                                        + userVerification.Identifier + " message:" + ex.Message);
                    }

                    return false;
                }

                return true;
            }

            return false;
        }
    }
}
