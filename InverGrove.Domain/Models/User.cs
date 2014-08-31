using System;
using InverGrove.Domain.Interfaces;
using Invergrove.Domain.Models;

namespace InverGrove.Domain.Models
{
    public class User : Resource, IUser
    {
        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the last activity date.
        /// </summary>
        /// <value>
        /// The last activity date.
        /// </value>
        public DateTime LastActivityDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is anonymous.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is anonymous; otherwise, <c>false</c>.
        /// </value>
        public bool IsAnonymous { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>
        /// The date modified.
        /// </value>
        public DateTime DateModified { get; set; }  
    }
}