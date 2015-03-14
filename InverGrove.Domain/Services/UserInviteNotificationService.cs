using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
namespace InverGrove.Domain.Services
{
    public class UserInviteNotificationService : IUserInviteNotificationService
    {

        /// <summary>
        /// Adds the user invite notice to the DB.
        /// NOT RESPONSIBLE for email.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool AddUserInviteNotice(int personId)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the user invite notice from the DB.
        /// </summary>
        /// <param name="personId">The person identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public Notification GetUserInviteNotice(int personId)
        {
            throw new System.NotImplementedException();
        }
    }
}