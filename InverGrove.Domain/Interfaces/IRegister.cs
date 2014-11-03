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
        /// Gets or sets the person.
        /// </summary>
        /// <value>
        /// The person.
        /// </value>
        Person Person { get; set; }

        /// <summary>
        /// Gets or sets the marital status list.
        /// </summary>
        /// <value>
        /// The marital status list.
        /// </value>
        IEnumerable<SelectListItem> MaritalStatusList { get; set; }

        /// <summary>
        /// Gets or sets the person type list.
        /// </summary>
        /// <value>
        /// The person type list.
        /// </value>
        IEnumerable<SelectListItem> PersonTypeList { get; set; }
    }
}