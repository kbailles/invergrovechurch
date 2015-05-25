using System.ComponentModel.DataAnnotations;

namespace InverGrove.Domain.Interfaces
{
    public interface IPhoneNumber
    {
        /// <summary>
        /// Gets or sets the phone number identifier.
        /// </summary>
        /// <value>
        /// The phone number identifier.
        /// </value>
        int PhoneNumberId { get; set; }

        /// <summary>
        /// Gets or sets the area code.
        /// </summary>
        /// <value>
        /// The area code.
        /// </value>
        [Required]
        [StringLength(3)]
        string AreaCode { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        [Required]
        [StringLength(7)]
        string Phone { get; set; }

        /// <summary>
        /// Gets or sets the phone number type identifier.
        /// </summary>
        /// <value>
        /// The phone number type identifier.
        /// </value>
        [Required]
        int PhoneNumberTypeId { get; set; }
    }
}