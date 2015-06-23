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
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>
        /// The person identifier.
        /// </value>
        int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        string Phone { get; set; }

        /// <summary>
        /// Gets or sets the phone number type identifier.
        /// </summary>
        /// <value>
        /// The phone number type identifier.
        /// </value>
        int PhoneNumberTypeId { get; set; }

        /// <summary>
        /// Home, Mobile, Work. Prose value of PhoneNumberTypeId
        /// </summary>
        /// <value>
        /// The type of the phone number.
        /// </value>
        string PhoneNumberType { get; set; }
    }
}