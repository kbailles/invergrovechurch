using InverGrove.Domain.Models;
using System;

namespace InverGrove.Domain.Interfaces
{
    public interface IUserVerificationService
    {
        bool AddUserInviteNotice(int personId);
        UserVerification GetUserInviteNotice(Guid identifier); 
    }
}