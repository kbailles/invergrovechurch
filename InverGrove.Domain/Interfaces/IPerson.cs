using System;
using System.Collections.Generic;
using InverGrove.Domain.Models;
using System.Web.Mvc;

namespace InverGrove.Domain.Interfaces
{
    public interface IPerson
    {
        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        /// <value>
        /// The person id.
        /// </value>
        int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        string LastName { get; set; }

        /// <summary>
        /// Gets or sets the middle initial.
        /// </summary>
        /// <value>
        /// The middle initial.
        /// </value>
        string MiddleInitial { get; set; }

        /// <summary>
        /// Gets or sets the address one.
        /// </summary>
        /// <value>
        /// The address one.
        /// </value>
        string AddressOne { get; set; }

        /// <summary>
        /// Gets or sets the address two.
        /// </summary>
        /// <value>
        /// The address two.
        /// </value>
        string AddressTwo { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        string State { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the primary email.
        /// </summary>
        /// <value>
        /// The primary email.
        /// </value>
        string PrimaryEmail { get; set; }

        /// <summary>
        /// Gets or sets the secondary email.
        /// </summary>
        /// <value>
        /// The secondary email.
        /// </value>
        string SecondaryEmail { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        /// <value>
        /// The phone numbers.
        /// </value>
        IList<PhoneNumber> PhoneNumbers { get; set; }

        /// <summary>
        /// Gets or sets the date of birth.
        /// </summary>
        /// <value>
        /// The date of birth.
        /// </value>
        DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the group photo file path.
        /// </summary>
        /// <value>
        /// The group photo file path.
        /// </value>
        string GroupPhotoFilePath { get; set; }

        /// <summary>
        /// Gets or sets the individual photo file path.
        /// </summary>
        /// <value>
        /// The individual photo file path.
        /// </value>
        string IndividualPhotoFilePath { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        string Gender { get; set; }

        /// <summary>
        /// Gets or sets the marital status id.
        /// </summary>
        /// <value>
        /// The marital status id.
        /// </value>
        int MaritalStatusId { get; set; }

        /// <summary>
        /// Gets or sets the church role identifier.
        /// </summary>
        /// <value>
        /// The church role identifier.
        /// </value>
        int? ChurchRoleId { get; set; }

        /// <summary>
        /// When creating a new member through the form, we decide 
        /// </summary>
        /// <value>
        /// The type of the account.
        /// </value>
        bool IsUser { get; set; }

        /// <summary>
        /// Gets or sets the marital status list.
        /// </summary>
        /// <value>
        /// The marital status list.
        /// </value>
        IEnumerable<SelectListItem> MaritalStatusList { get; set; }

        /// <summary>
        /// Gets or sets the church role list.
        /// </summary>
        /// <value>
        /// The church role list.
        /// </value>
        IEnumerable<SelectListItem> ChurchRoleList { get; set; }

    }
}