using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
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

        /// <summary>
        /// Gets or sets a value indicating whether this instance is baptized.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is baptized; otherwise, <c>false</c>.
        /// </value>
        [Required]
        [Display(ResourceType = typeof(ViewLabels), Name = "IsBaptizedLabel")]
        public bool IsBaptized { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is local.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is local; otherwise, <c>false</c>.
        /// </value>
        [Required]
        [Display(ResourceType = typeof(ViewLabels), Name = "IsLocalLabel")]
        public bool IsLocal { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        [Required]
        [Display(ResourceType = typeof(ViewLabels), Name = "IsActiveLabel")]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        /// <value>
        /// The person.
        /// </value>
        public Person Person { get; set; }

        /// <summary>
        /// Gets or sets the marital status id.
        /// </summary>
        /// <value>
        /// The marital status id.
        /// </value>
        public int MaritalStatusId
        {
            get
            {
                if (this.Person == null)
                {
                    this.Person = ObjectFactory.Create<Person>();
                }

                return this.Person.MaritalStatusId;
            }
            set
            {
                if (this.Person == null)
                {
                    this.Person = ObjectFactory.Create<Person>();
                }

                this.Person.MaritalStatusId = value;
            }
        }

        /// <summary>
        /// Gets or sets the person type id.
        /// </summary>
        /// <value>
        /// The person type id.
        /// </value>
        public int PersonTypeId
        {
            get
            {
                if (this.Person == null)
                {
                    this.Person = ObjectFactory.Create<Person>();
                }

                return this.Person.PersonTypeId;
            }
            set
            {
                if (this.Person == null)
                {
                    this.Person = ObjectFactory.Create<Person>();
                }

                this.Person.PersonTypeId = value;
            }
        }

        /// <summary>
        /// Gets or sets the marital status list.
        /// </summary>
        /// <value>
        /// The marital status list.
        /// </value>
        public IEnumerable<SelectListItem> MaritalStatusList { get; set; }

        /// <summary>
        /// Gets or sets the person type list.
        /// </summary>
        /// <value>
        /// The person type list.
        /// </value>
        public IEnumerable<SelectListItem> PersonTypeList { get; set; }
    }
}