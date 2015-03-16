using InverGrove.Data.Entities;

namespace InverGrove.Domain.Interfaces
{
    public interface INotificationRepository
    {
        bool AddUserInviteNotice(int personId);
        UserVerification GetUserInviteNotice(int personId);  
    }
}