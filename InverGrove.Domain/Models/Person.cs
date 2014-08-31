using System;
using InverGrove.Domain.Interfaces;
using Invergrove.Domain.Models;

namespace InverGrove.Domain.Models
{
    public class Person : Resource, IPerson
    {
        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        /// <value>
        /// The person id.
        /// </value>
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the address one.
        /// </summary>
        /// <value>
        /// The address one.
        /// </value>
        public string AddressOne { get; set; }

        /// <summary>
        /// Gets or sets the address two.
        /// </summary>
        /// <value>
        /// The address two.
        /// </value>
        public string AddressTwo { get; set; }

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
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the primary email.
        /// </summary>
        /// <value>
        /// The primary email.
        /// </value>
        public string PrimaryEmail { get; set; }

        /// <summary>
        /// Gets or sets the secondary email.
        /// </summary>
        /// <value>
        /// The secondary email.
        /// </value>
        public string SecondaryEmail { get; set; }

        /// <summary>
        /// Gets or sets the primary phone.
        /// </summary>
        /// <value>
        /// The primary phone.
        /// </value>
        public string PrimaryPhone { get; set; }

        /// <summary>
        /// Gets or sets the secondary phone.
        /// </summary>
        /// <value>
        /// The secondary phone.
        /// </value>
        public string SecondaryPhone { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the group photo file path.
        /// </summary>
        /// <value>
        /// The group photo file path.
        /// </value>
        public string GroupPhotoFilePath { get; set; }

        /// <summary>
        /// Gets or sets the individual photo file path.
        /// </summary>
        /// <value>
        /// The individual photo file path.
        /// </value>
        public string IndividualPhotoFilePath { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the marital status id.
        /// </summary>
        /// <value>
        /// The marital status id.
        /// </value>
        public int MaritalStatusId { get; set; }

        /// <summary>
        /// Gets or sets the person type id.
        /// </summary>
        /// <value>
        /// The person type id.
        /// </value>
        public int PersonTypeId { get; set; }
    }
}