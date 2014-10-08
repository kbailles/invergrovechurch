using System;
using System.Net.Mail;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Services
{
    public class EmailService : IEmailService
    {


        public EmailService()
        {
            // stubbed
        }

        public bool SendContactMail(IContact contact)
        {
            // (1) assemble mailMessage
            // (2) invoke SnedMail(args)
            // (3) update DB for mail sent.

            return false;
        }

        private bool SendMail(MailMessage mailMessage)
        {
            Guard.ArgumentNotNull(mailMessage, "MailMessage");

            SmtpClient smtp = new SmtpClient(/*emailServer*/);

            try
            {
                smtp.Send(mailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception("Email client failed to send group email. " + ex.Message);
            }
        }
    }
}