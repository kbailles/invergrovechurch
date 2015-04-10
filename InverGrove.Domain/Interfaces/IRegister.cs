using System;
using System.Collections.Generic;
using System.Web.Mvc;
using InverGrove.Domain.Models;

namespace InverGrove.Domain.Interfaces
{
    public interface IRegister
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>
        /// The person identifier.
        /// </value>
        int PersonId { get; set; }

        /// <summary>
        /// GUID for the person, since we don't want the PesonId to be tampered with 
        /// in the client.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        Guid Identifier { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        /// <value>
        /// The user email.
        /// </value>
        string UserEmail { get; set; }

    }
}