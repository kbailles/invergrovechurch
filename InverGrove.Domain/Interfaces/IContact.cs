using System;

namespace InverGrove.Domain.Interfaces
{
    public interface IContact
    {
        int ContactsId { get; set;}
        string Name { get; set; }
        string Address { get; set; }
        string City { get; set; }
        string State { get; set; }
        string Zip { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        bool IsVisitorCard { get; set; }
        bool IsOnlineContactForm { get; set; }
        string Comments { get; set; }
        DateTime DateSubmitted { get; set; }
    }
}
