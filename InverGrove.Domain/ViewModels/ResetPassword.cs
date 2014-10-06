using System.ComponentModel.DataAnnotations;
using InverGrove.Domain.Resources;

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
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ViewLabels), Name = "ConfirmPasswordLabel")]
        [Compare("Password", ErrorMessageResourceType = typeof (Messages), ErrorMessageResourceName = "ConfirmPasswordErrorMessage")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }

    }
}