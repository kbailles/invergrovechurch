using System;
using System.ComponentModel.DataAnnotations;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Models
{
    public class Contact : IContact
    {
        public int ContactsId { get; set; } // <-- should be read-only, not exposed at all, or substituted with a GUID ?
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsVisitorCard { get; set; }
        public bool IsOnlineContactForm { get; set; }
        public string Subject { get; set; }
        public string Comments { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}
