using System;

namespace InverGrove.Domain.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the last activity date.
        /// </summary>
        /// <value>
        /// The last activity date.
        /// </value>
        DateTime LastActivityDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is anonymous.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is anonymous; otherwise, <c>false</c>.
        /// </value>
        bool IsAnonymous { get; set; }

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
    }
}