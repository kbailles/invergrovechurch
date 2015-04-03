﻿using System;
using System.Linq;
using InverGrove.Data;
using Invergrove.Domain.Models;
using InverGrove.Domain.Repositories;
using InverGrove.Domain.Utils;
using System.Data.SqlClient;

namespace InverGrove.Domain.Interfaces
{
    public class UserVerificationRepository : EntityRepository<Data.Entities.UserVerification, int>, IUserVerificationRepository
    {
        public UserVerificationRepository(IInverGroveContext dataContext) 
            : base(dataContext)
        {
        }

        /// <summary>
        /// Adds the specified person identifier.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        public Guid Add(int personId)
        {
            Guard.ParameterNotGreaterThanZero(personId, "personId");

            var userVerification = new Data.Entities.UserVerification
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
                throw new ApplicationException("Error when attempting to add new UserVerificatioin in UserVerificationRepository: " + ex.Message);
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
                      where uv.Identifier.Equals(identifier)
                      select new Models.UserVerification { Identifier = uv.Identifier,
                                                           DateSent = uv.DateSent,
                                                           DateAccessed = (DateTime)uv.DateAccessed,
                                                           PersonName = pr.FirstName + " " + pr.LastName
                                                        }).FirstOrDefault();
            return userVerification;
        }
    }
}
