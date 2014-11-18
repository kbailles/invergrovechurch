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
        /// <param name="registeredUser">The registered user.</param>
        /// <returns></returns>
        bool SendNewUserEmail(IRegister registeredUser);
    }
}
