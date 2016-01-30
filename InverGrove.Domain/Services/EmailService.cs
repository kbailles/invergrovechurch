using System;
using System.Net.Mail;
using System.Text;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogService logService;
        private const string ToAddress = "lancebailles@hotmail.com;heidibailles@hotmail.com;kbailles@outlook.com";
        private string DefaultBaseHost = "http://www.invergrovechurch.com";

        public EmailService()//ILogService logService
        {
            this.logService = null; //logService;
        }

        /// <summary>
        /// Sends the contact mail.
        /// </summary>
        /// <param name="contact">The contact.</param>
        /// <returns></returns>
        public bool SendContactMail(IContact contact)
        {
            Guard.ParameterNotNull(contact, "contact");

            MailMessage mailMesage = new MailMessage
                                     {
                                         From = new MailAddress(contact.Email),
                                         Subject =  contact.Subject,
                                         Body = contact.Comments
                                     };

            foreach (var address in ToAddress.Split(new [] {";"}, StringSplitOptions.RemoveEmptyEntries))
            {
                mailMesage.To.Add(address); // will not let me do this in object builder
            }

            return this.SendMail(mailMesage);
        }

        /// <summary>
        /// Sends the new user email.
        /// </summary>
        /// <param name="personToRegister">The registered user.</param>
        /// <param name="userVerificationId">The user verification identifier.</param>
        /// <param name="hostName">Name of the host.</param>
        /// <returns></returns>
        public bool SendNewUserEmail(IPerson personToRegister, Guid userVerificationId, string hostName)
        {
            Guard.ParameterNotNull(personToRegister, "personToRegister");

            var message = new StringBuilder();

            if (string.IsNullOrEmpty(hostName))
            {
                hostName = this.DefaultBaseHost;
            }

            if (!hostName.StartsWith("http"))
            {
                hostName = "http://" + hostName;
            }

            message.Append(personToRegister.FirstName);
            message.Append(",");
            message.Append("<br><br>");
            message.Append("This is a message to inform you that a new user account is ready to be created");
            message.Append(" for you to access the member area at " + hostName + ".");
            message.Append("<br> Please click on the following link to access the site and add your user name and password: ");
            message.Append(hostName + "/Account/Register?code=");
            message.Append(userVerificationId);

            var mailMessage = new MailMessage
                                {
                                    IsBodyHtml = true,
                                    Subject = "Inver Grove Church Notification",
                                    Body = message.ToString()
                                };

            mailMessage.To.Add(personToRegister.PrimaryEmail);

            return this.SendMail(mailMessage);
        }

        private bool SendMail(MailMessage mailMessage)
        {
            Guard.ParameterNotNull(mailMessage, "mailMessage");
            var success = true;

            var smtpClient = new SmtpClient { DeliveryMethod = SmtpDeliveryMethod.Network };//  or "localhost"

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                success = false;

                if (this.logService != null)
                {
                    this.logService.WriteToErrorLog("Email client failed to send email with subject: " + mailMessage.Subject +
                                                    " error message: " + ex.Message);
                }
            }

            return success;
        }
    }
}