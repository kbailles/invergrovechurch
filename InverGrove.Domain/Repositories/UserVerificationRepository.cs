using System;
using InverGrove.Data;
using InverGrove.Domain.Models;
using InverGrove.Domain.Repositories;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Interfaces
{
    public class UserVerificationRepository : EntityRepository<Data.Entities.UserVerification, int>, IUserVerificationRepository
    {


        public UserVerificationRepository(IInverGroveContext dataContext) 
            : base(dataContext)
        {
        }

        public Guid Add(int personId)
        {
            Guard.ParameterNotGreaterThanZero(personId, "personId");

            var userVerification = new Data.Entities.UserVerification
                                   {
                                       PersonId = personId,
                                       Identifier = Guid.NewGuid(),
                                       DateSent = DateTime.Now,
                                       DateAccessed = DateTime.MinValue /* not nullable */
                                   };

            this.Insert(userVerification);

            // TODO - TryCatch

            this.dataContext.Commit();

            return userVerification.Identifier;

        }

    }
}
