using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogService logService;
        private const string ToAddress = "lancebailles@hotmail.com";

        public EmailService(ILogService logService)
        {
            this.logService = logService;
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

            mailMesage.To.Add(ToAddress); // will not let me do this in object builder

            return this.SendMail(mailMesage);
        }

        /// <summary>
        /// Sends the new user email.
        /// </summary>
        /// <param name="registeredUser">The registered user.</param>
        /// <returns></returns>
        public bool SendNewUserEmail(IRegister registeredUser)
        {
            Guard.ParameterNotNull(registeredUser, "registeredUser");

            var message = new StringBuilder();

            if (registeredUser.Person != null)
            {
                message.Append(registeredUser.Person.FirstName);
                message.Append(",");
                message.Append("<br><br>");
                message.Append(
                    "This is a message to inform you that a new user account has been added for you to access the member area at http://wwww.invergrovechurch.com.");
                message.Append(
                    "<br> Please click on the following link to access the site and change your password: http://www.invergrove.com/Account/ResetPassword?code=");
                message.Append(registeredUser.Password);

                var mailMessage = new MailMessage
                                  {
                                      IsBodyHtml = true,
                                      Subject = "Inver Grove Church Notification",
                                      Body = message.ToString()
                                  };

                mailMessage.To.Add(registeredUser.Person.PrimaryEmail);

                return this.SendMail(mailMessage);
            }

            return false;
        }

        private bool SendMail(MailMessage mailMessage)
        {
            Guard.ParameterNotNull(mailMessage, "mailMessage");
            bool success = true;

            SmtpClient smtpClient = new SmtpClient {DeliveryMethod = SmtpDeliveryMethod.Network};

            //
            //                        {
            //                            Credentials = new NetworkCredential("you@InverGroveChurch.com", "glock34")
            //                        }; //  or "localhost", "mail.InverGroveChurch.com"

            try
            {
                smtpClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                success = false;
                this.logService.WriteToErrorLog("Email client failed to send email with subject: " + mailMessage.Subject + " error message: " + ex.Message);
            }

            return success;
        }
    }
}