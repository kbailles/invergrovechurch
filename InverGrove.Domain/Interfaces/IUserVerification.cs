using System;

namespace InverGrove.Domain.Interfaces
{
    public interface IUserVerification
    {
        /// <summary>
        /// Gets or sets the user verification identifier.
        /// </summary>
        /// <value>
        /// The user verification identifier.
        /// </value>
        int UserVerificationId { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>
        /// The person identifier.
        /// </value>
        int PersonId { get; set; }

        /// <summary>
        /// Auto-generated GUID, serves as access token when notification goes out.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Identifier { get; set; }

        /// <summary>
        /// Gets or sets the date sent.
        /// </summary>
        /// <value>
        /// The date sent.
        /// </value>
        DateTime DateSent { get; set; }

        /// <summary>
        /// Gets or sets the date accessed.
        /// </summary>
        /// <value>
        /// The date accessed.
        /// </value>
        DateTime DateAccessed { get; set; }

        /// <summary>
        /// Gets or sets the name of the person.
        /// </summary>
        /// <value>
        /// The name of the person.
        /// </value>
        string PersonName { get; set; }
    }
}
