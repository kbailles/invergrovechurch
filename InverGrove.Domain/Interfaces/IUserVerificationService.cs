using InverGrove.Domain.Models;

namespace InverGrove.Domain.Interfaces
{
    public interface IUserVerificationService
    {
        bool AddUserInviteNotice(int personId);
        UserVerification GetUserInviteNotice(int personId); 
    }
}