using InverGrove.Data;
using InverGrove.Domain.Repositories;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Interfaces
{
    public class UserInviteNotificationRepository : EntityRepository<Data.Entities.UserVerification, int>, IUserInviteNotificationRepository
    {

        public UserInviteNotificationRepository(IInverGroveContext dataContext) 
            : base(dataContext)
        {
        }

        public bool Add(int personId)
        {
            Guard.ParameterNotGreaterThanZero(personId, "personId");

            return false;
        }

    }
}
