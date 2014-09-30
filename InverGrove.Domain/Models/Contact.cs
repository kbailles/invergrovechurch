using System;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Models
{
    public class Contact : IContact
    {
        /// <summary>
        /// Gets or sets the contacts identifier.
        /// </summary>
        /// <value>
        /// The contacts identifier.
        /// </value>
        public int ContactsId { get; set; } // <-- should be read-only, not exposed at all, or substituted with a GUID ?

        public Contact(int contactsId, string name, string address, string city, string state, string zip, string email, string phone, bool isVisitorCard, bool isOnlineContactForm, string comments, DateTime dateSubmitted)
        {
            this.ContactsId = contactsId;
            this.Name = name;
            this.Address = address;
            this.City = city;
            this.State = state;
            this.Zip = zip;
            this.Email = email;
            this.Phone = phone;
            this.IsVisitorCard = isVisitorCard;
            this.IsOnlineContactForm = isOnlineContactForm;
            this.Comments = comments;
            this.DateSubmitted = dateSubmitted;
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>
        /// The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the zip.
        /// </summary>
        /// <value>
        /// The zip.
        /// </value>
        public string Zip { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is visitor card.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is visitor card; otherwise, <c>false</c>.
        /// </value>
        public bool IsVisitorCard { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is online contact form.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is online contact form; otherwise, <c>false</c>.
        /// </value>
        public bool IsOnlineContactForm { get; set; }

        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Comments { get; set; }

        /// <summary>
        /// Pretty useless? 
        /// </summary>
        /// <value>
        /// The date submitted.
        /// </value>
        public DateTime DateSubmitted { get; set; }
    }
}
