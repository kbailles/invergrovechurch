using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using InverGrove.Domain.Interfaces;
using Invergrove.Domain.Models;
using InverGrove.Domain.Resources;
using InverGrove.Domain.ValueTypes;

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
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FirstNameRequired")]
        [Display(ResourceType = typeof(ViewLabels), Name = "FirstNameLabel")]
        [StringLength(64, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FirstLastNameLengthError", MinimumLength = 1)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "LastNameRequired")]
        [Display(ResourceType = typeof(ViewLabels), Name = "LastNameLabel")]
        [StringLength(64, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FirstLastNameLengthError", MinimumLength = 1)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the middle initial.
        /// </summary>
        /// <value>
        /// The middle initial.
        /// </value>
        [Display(ResourceType = typeof(ViewLabels), Name = "MiddleInitialLabel")]
        public string MiddleInitial { get; set; }

        /// <summary>
        /// Gets or sets the address one.
        /// </summary>
        /// <value>
        /// The address one.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AddressOneRequired")]
        [Display(ResourceType = typeof(ViewLabels), Name = "AddressOneLabel")]
        [StringLength(200, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AddressLengthErrorMessage", MinimumLength = 1)]
        public string AddressOne { get; set; }

        /// <summary>
        /// Gets or sets the address two.
        /// </summary>
        /// <value>
        /// The address two.
        /// </value>
        [Display(ResourceType = typeof(ViewLabels), Name = "AddressTwoLabel")]
        [StringLength(200, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AddressLengthErrorMessage", MinimumLength = 1)]
        public string AddressTwo { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        /// <value>
        /// The city.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "CityRequired")]
        [Display(ResourceType = typeof(ViewLabels), Name = "CityLabel")]
        [StringLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "CityLengthErrorMessage", MinimumLength = 1)]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "StateRequired")]
        [Display(ResourceType = typeof(ViewLabels), Name = "StateLabel")]
        public string State { get; set; }

        /// <summary>
        /// Gets or sets the zip code.
        /// </summary>
        /// <value>
        /// The zip code.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PostalCodeRequired")]
        [Display(ResourceType = typeof(ViewLabels), Name = "ZipCodeLabel")]
        [RegularExpression(RegularExpressions.PasswordRegEx, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PostalCodeInvalidErrorMessage")]
        public string ZipCode { get; set; }

        /// <summary>
        /// Gets or sets the primary email.
        /// </summary>
        /// <value>
        /// The primary email.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "EmailRequiredErrorMessage")]
        [Display(ResourceType = typeof(ViewLabels), Name = "EmailLabel")]
        [EmailAddress]
        public string PrimaryEmail { get; set; }

        /// <summary>
        /// Gets or sets the secondary email.
        /// </summary>
        /// <value>
        /// The secondary email.
        /// </value>
        [Display(ResourceType = typeof(ViewLabels), Name = "SecondaryEmailLabel")]
        [EmailAddress]
        public string SecondaryEmail { get; set; }

        /// <summary>
        /// Gets or sets the phone numbers.
        /// </summary>
        /// <value>
        /// The phone numbers.
        /// </value>
        public IList<PhoneNumber> PhoneNumbers { get; set; }

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
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "GenderRequiredErrorMessage")]
        [Display(ResourceType = typeof(ViewLabels), Name = "GenderLabel")]
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the marital status id.
        /// </summary>
        /// <value>
        /// The marital status id.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "MaritalStatusRequiredErrorMessage")]
        [Display(ResourceType = typeof(ViewLabels), Name = "MaritalStatusLabel")]
        public int MaritalStatusId { get; set; }

        /// <summary>
        /// Gets or sets the person type id.
        /// </summary>
        /// <value>
        /// The person type id.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PersonTypeRequiredErrorMessage")]
        [Display(ResourceType = typeof(ViewLabels), Name = "PersonTypeLabel")]
        public int PersonTypeId { get; set; }
    }
}