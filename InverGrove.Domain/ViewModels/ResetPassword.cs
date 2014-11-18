using System.ComponentModel.DataAnnotations;
using InverGrove.Domain.Resources;
using InverGrove.Domain.ValueTypes;

namespace InverGrove.Domain.ViewModels
{
    public class ResetPassword
    {
        [Required]
        [Display(ResourceType = typeof(ViewLabels), Name = "UserNameLabel")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PasswordErrorMessage", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ViewLabels), Name = "PasswordLabel")]
        [RegularExpression(RegularExpressions.PasswordRegEx, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ChangePasswordNotCorrect")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ViewLabels), Name = "ConfirmPasswordLabel")]
        [Compare("Password", ErrorMessageResourceType = typeof (Messages), ErrorMessageResourceName = "ConfirmPasswordErrorMessage")]
        [RegularExpression(RegularExpressions.PasswordRegEx, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ChangePasswordNotCorrect")]
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

        public string Code { get; set; }

    }
}