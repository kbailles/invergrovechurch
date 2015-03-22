using System;

namespace InverGrove.Domain.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends the contact mail.
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <returns></returns>
        bool SendContactMail(IContact contact);

        /// <summary>
        /// Sends the new user email.
        /// </summary>
        /// <param name="personToRegister">The person to register.</param>
        /// <param name="userVerificationId">The user verification identifier.</param>
        /// <returns></returns>
        bool SendNewUserEmail(IPerson personToRegister, Guid userVerificationId);
    }
}
