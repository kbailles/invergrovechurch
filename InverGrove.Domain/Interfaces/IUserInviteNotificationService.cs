using InverGrove.Domain.Models;

namespace InverGrove.Domain.Interfaces
{
    public interface IUserInviteNotificationService
    {
        bool AddUserInviteNotice(int personId);
        Notification GetUserInviteNotice(int personId); 
    }
}