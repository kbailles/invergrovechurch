using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Resources;

namespace InverGrove.Domain.ViewModels
{
    public class Register : IRegister
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        [Display(ResourceType = typeof(ViewLabels), Name = "UserNameLabel")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PasswordErrorMessage", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ViewLabels), Name = "PasswordLabel")]
        public string Password { get; set; }

        ///// <summary>
        ///// Gets or sets the confirm password.
        ///// </summary>
        ///// <value>
        ///// The confirm password.
        ///// </value>
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ViewLabels), Name = "ConfirmPasswordLabel")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ConfirmPasswordErrorMessage")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>
        /// The person identifier.
        /// </value>
        public int PersonId { get; set; }

        /// <summary>
        /// GUID for the person, since we don't want the PesonId to be tampered with
        /// in the client.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public Guid Identifier { get; set; }

        /// <summary>
        /// Gets or sets the user email.
        /// </summary>
        /// <value>
        /// The user email.
        /// </value>
        public string UserEmail { get; set; }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        /// <value>
        /// The roles.
        /// </value>
        public IEnumerable<SelectListItem> Roles { get; set; }
    }
}