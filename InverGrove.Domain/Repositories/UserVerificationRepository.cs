using System;
using System.Linq;
using InverGrove.Data;
using Invergrove.Domain.Models;
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
                                       DateSent = DateTime.Now
                                   };

            this.Insert(userVerification);

            // TODO - TryCatch

            this.dataContext.Commit();

            return userVerification.Identifier;

        }


        public IUserVerification Get(Guid identifier)
        {

            var userVerification = from uv in this.dataContext.UserVerifications
                      join pr in this.dataContext.People
                      on uv.PersonId equals pr.PersonId
                      where uv.Identifier.Equals(identifier)
                      select new Models.UserVerification { Identifier = uv.Identifier,
                                                           DateSent = uv.DateSent,
                                                           DateAccessed = (DateTime)uv.DateAccessed,
                                                           PersonName = pr.FirstName + " " + pr.LastName
                                                        };
            if (userVerification != null)
            {
                return userVerification;
            }

            return userVerification;
        }
    }
}
