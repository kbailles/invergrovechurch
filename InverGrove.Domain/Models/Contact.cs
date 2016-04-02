using System;
using System.ComponentModel.DataAnnotations;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Models
{
    public class Contact : IContact
    {
        public int ContactsId { get; set; } // <-- should be read-only, not exposed at all, or substituted with a GUID ?
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters!")]
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters!")]
        public string Email { get; set; }
        public string Phone { get; set; }

        public bool IsVisitorCard { get; set; }
        public bool IsOnlineContactForm { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters!")]
        public string Subject { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 500 characters!")]
        public string Comments { get; set; }
        public DateTime DateSubmitted { get; set; }
    }
}
