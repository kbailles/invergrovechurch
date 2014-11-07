using System;
using System.Net.Mail;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Services
{
    public class EmailService : IEmailService
    {
        private MailAddress fromAddress = new MailAddress("DoNotReply@nowhere.com");



        public EmailService()
        {
            // stubbed
        }

        public bool SendContactMail(IContact contact)
        {
            Guard.ArgumentNotNull(contact, "contact");

            // (1) assemble mailMessage
            MailMessage mailMesage = new MailMessage
                                     {
                                         From = this.fromAddress,
                                         Subject =  contact.Subject,
                                         Body = contact.Comments
                                     };

            mailMesage.To.Add(contact.Email); // will not let me do this in object builder
            bool isSent = this.SendMail(mailMesage);

            // --------- TODO -------------------------
            //---------- UPDATE THE DB FOR MESSGE SENT
            // ----------------------------------------

            if (isSent)
            {
                return true;
            }


            return false;
        }

        private bool SendMail(MailMessage mailMessage)
        {
            Guard.ArgumentNotNull(mailMessage, "MailMessage");


            SmtpClient smtpClient = new SmtpClient("localhost"); //  or 127.0.0.1

            try
            {
                //smtpClient.Send(mailMessage);
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