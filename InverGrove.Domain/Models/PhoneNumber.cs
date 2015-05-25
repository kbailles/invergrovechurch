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
        /// Gets or sets the area code.
        /// </summary>
        /// <value>
        /// The area code.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AreaCodeRequired")]
        [StringLength(3)]
        [RegularExpression(RegularExpressions.AreaCodeRegEx, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AreaCodeInvalidErrorMessage")]
        public string AreaCode { get; set; }

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
    }
}