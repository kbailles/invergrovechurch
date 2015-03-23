using System;

namespace InverGrove.Domain.Interfaces
{
    /// <summary>
    /// Creates a notification record in the db for people being added to church directory
    /// who will also be website users.
    /// </summary>
    public interface IUserVerificationRepository
    {

        Guid Add(int personId);
    }
}