using System.ComponentModel.DataAnnotations;
using InverGrove.Domain.Resources;

namespace InverGrove.Domain.ViewModels
{
    public class Register
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
        /// Gets or sets the confirm password.
        /// </summary>
        /// <value>
        /// The confirm password.
        /// </value>
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ViewLabels), Name = "ConfirmPasswordLabel")]
        [Compare("Password", ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ConfirmPasswordErrorMessage")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets the password question.
        /// </summary>
        /// <value>
        /// The password question.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SecurityQuestionRequired")]
        [Display(ResourceType = typeof(ViewLabels), Name = "PasswordQuestionLabel")]
        [StringLength(64, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SecurityQuestionMaximumLength", MinimumLength = 6)]
        public string PasswordQuestion { get; set; }

        /// <summary>
        /// Gets or sets the password answer.
        /// </summary>
        /// <value>
        /// The password answer.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SecurityAnswerRequired")]
        [Display(ResourceType = typeof(ViewLabels), Name = "PasswordAnswerLabel")]
        [StringLength(64, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "SecurityAnswerMaximumLength", MinimumLength = 6)]
        public string PasswordAnswer { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FirstNameRequired")]
        [Display(ResourceType = typeof(ViewLabels), Name = "FirstNameLabel")]
        [StringLength(64, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FirstLastNameLengthError", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "LastNameRequired")]
        [Display(ResourceType = typeof(ViewLabels), Name = "LastNameLabel")]
        [StringLength(64, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "FirstLastNameLengthError", MinimumLength = 1)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the middle initial.
        /// </summary>
        /// <value>
        /// The middle initial.
        /// </value>
        [Display(ResourceType = typeof(ViewLabels), Name = "MiddleInitialLabel")]
        public string MiddleInitial { get; set; }
    }
}