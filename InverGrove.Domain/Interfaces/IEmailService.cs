using System;


namespace InverGrove.Domain.Interfaces
{
    public interface IEmailService
    {
        bool SendContactMail(IContact contact);
    }
}
