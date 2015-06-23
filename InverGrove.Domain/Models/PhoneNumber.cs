using System.ComponentModel.DataAnnotations;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Resources;
using InverGrove.Domain.ValueTypes;

namespace InverGrove.Domain.Models
{
    public class PhoneNumber : IPhoneNumber
    {
        /// <summary>
        /// Gets or sets the phone number identifier.
        /// </summary>
        /// <value>
        /// The phone number identifier.
        /// </value>
        public int PhoneNumberId { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>
        /// The person identifier.
        /// </value>
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PhoneNumberRequiredErrorMessage")]
        [StringLength(7, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PhoneNumberFormatErrorMessage")]
        [Phone]
        [Display(ResourceType = typeof(ViewLabels), Name = "PhoneNumberLabel")]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the phone number type identifier.
        /// </summary>
        /// <value>
        /// The phone number type identifier.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PhoneNumberTypeRequired")]
        public int PhoneNumberTypeId { get; set; }

        public string PhoneNumberType { get; set; }
    }
}