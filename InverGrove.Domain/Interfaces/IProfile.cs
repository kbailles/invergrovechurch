using System;

namespace InverGrove.Domain.Interfaces
{
    public interface IProfile
    {
        /// <summary>
        /// Gets or sets the profile id.
        /// </summary>
        /// <value>
        /// The profile id.
        /// </value>
        int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        int UserId { get; set; }

        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        /// <value>
        /// The person id.
        /// </value>
        int PersonId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [receive email notification].
        /// </summary>
        /// <value>
        /// <c>true</c> if [receive email notification]; otherwise, <c>false</c>.
        /// </value>
        bool ReceiveEmailNotification { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is baptized.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is baptized; otherwise, <c>false</c>.
        /// </value>
        bool IsBaptized { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is local.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is local; otherwise, <c>false</c>.
        /// </value>
        bool IsLocal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is disabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is disabled; otherwise, <c>false</c>.
        /// </value>
        bool IsDisabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is validated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is validated; otherwise, <c>false</c>.
        /// </value>
        bool IsValidated { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>
        /// The date modified.
        /// </value>
        DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        /// <value>
        /// The person.
        /// </value>
        IPerson Person { get; set; }
    }
}