using System;
using System.Net.Mail;
using System.Text;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailAddress fromAddress = new MailAddress("DoNotReply@nowhere.com");

        public bool SendContactMail(IContact contact)
        {
            Guard.ParameterNotNull(contact, "contact");

            // (1) assemble mailMessage
            MailMessage mailMesage = new MailMessage
                                     {
                                         From = this.fromAddress,
                                         Subject =  contact.Subject,
                                         Body = contact.Comments
                                     };

            mailMesage.To.Add(contact.Email); // will not let me do this in object builder
            bool isSent = true;

            try
            {
                this.SendMail(mailMesage);
            }
            catch (Exception)
            {
                isSent = false;
            }

            // --------- TODO -------------------------
            //---------- UPDATE THE DB FOR MESSGE SENT
            // ----------------------------------------

            return isSent;
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
                message.Append("This is a message to inform you that a new user account has been added for you to access the member area at http://wwww.invergrovechurch.com.");
                message.Append("<br> Please click on the following link to access the site and change your password: http://www.invergrove.com/Account/ResetPassword?code=");
                message.Append(registeredUser.Password);

                var mailMessage = new MailMessage
                {
                    IsBodyHtml = true,
                    Subject = "Inver Grove Church Notification",
                    From = fromAddress,
                    Body = message.ToString()
                };

                mailMessage.To.Add(registeredUser.Person.PrimaryEmail);

                try
                {
                    this.SendMail(mailMessage);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return true;
        }

        private void SendMail(MailMessage mailMessage)
        {
            Guard.ParameterNotNull(mailMessage, "MailMessage");


            SmtpClient smtpClient = new SmtpClient("localhost"); //  or 127.0.0.1

            try
            {
                smtpClient.Send(mailMessage);
                //return true;
            }
            catch (Exception ex)
            {
                //return false; // should add logging here...
                throw new ApplicationException("Email client failed to send group email. " + ex.Message);
            }
        }
    }
}