using System;

namespace InverGrove.Domain.Interfaces
{
    public interface IUserVerificationService
    {
        bool AddUserInviteNotice(int personId);
        IUserVerification GetUserInviteNotice(Guid identifier); 
    }
}