using InverGrove.Data;
using InverGrove.Domain.Repositories;

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
            throw new System.NotImplementedException();
        }

    }
}
