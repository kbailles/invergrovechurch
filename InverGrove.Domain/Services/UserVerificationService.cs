using System;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Services
{
    public class UserVerificationService : IUserVerificationService
    {
        private readonly IUserVerificationRepository repository;

        public UserVerificationService(IUserVerificationRepository repository)
        {
            this.repository = repository;
        }

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
        /// Gets the user invite notice when the person has followed a link to the website
        /// contaning their guid. This method checks the validity of that request and returns
        /// a slim data payload.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns></returns>
        public IUserVerification GetUserInviteNotice(Guid identifier)
        {
            Guard.ParameterGuidNotEmpty(identifier, "identifier");

            var userVerification = this.repository.Get(identifier);

            return userVerification;
        }

        /// <summary>
        /// Updates the user invite notice.
        /// </summary>
        /// <param name="userVerification">The user verification.</param>
        /// <returns></returns>
        public bool UpdateUserInviteNotice(IUserVerification userVerification)
        {
            Guard.ArgumentNotNull(userVerification, "userVerification");

            userVerification.DateAccessed = DateTime.Now;

            return this.repository.Update(userVerification);
        }
    }
}