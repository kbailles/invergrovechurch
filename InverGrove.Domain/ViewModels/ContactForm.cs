using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.ViewModels
{
    public class ContactForm : IContact
    {
        public bool SuccessfullySentMessage { get; set; }

        public bool MessageSentFailure { get; set; }

        public int ContactsId { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(100, ErrorMessage = "This field cannot exceed 100 characters!")]
        public string Name { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Zip { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(100, ErrorMessage = "This field cannot exceed 100 characters!")]
        [EmailAddress(ErrorMessage = "This field must be a valid email address!")]
        public string Email { get; set; }

        public string Phone { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(100, ErrorMessage = "This field cannot exceed 100 characters!")]
        public string Subject { get; set; }

        public bool IsVisitorCard { get; set; }

        public bool IsOnlineContactForm { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [StringLength(500, ErrorMessage = "This field cannot exceed 500 characters!")]
        public string Comments { get; set; }

        public DateTime DateSubmitted { get; set; }
    }
}
