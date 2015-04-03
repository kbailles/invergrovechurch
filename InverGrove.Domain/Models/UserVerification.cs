using System;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Models
{
    public class UserVerification : IUserVerification
    {
        /// <summary>
        /// Gets or sets the user verification identifier.
        /// </summary>
        /// <value>
        /// The user verification identifier.
        /// </value>
        public int UserVerificationId { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>
        /// The person identifier.
        /// </value>
        public int PersonId { get; set; }

        /// <summary>
        /// Auto-generated GUID, serves as access token when notification goes out.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Identifier { get; set; }

        /// <summary>
        /// Gets or sets the date sent.
        /// </summary>
        /// <value>
        /// The date sent.
        /// </value>
        public DateTime DateSent { get; set; }

        /// <summary>
        /// Gets or sets the date accessed.
        /// </summary>
        /// <value>
        /// The date accessed.
        /// </value>
        public DateTime DateAccessed { get; set; }

        /// <summary>
        /// Gets or sets the name of the person.
        /// </summary>
        /// <value>
        /// The name of the person.
        /// </value>
        public string PersonName { get; set; }
    }
}