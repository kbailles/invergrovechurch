using System.ComponentModel.DataAnnotations;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Resources;

namespace InverGrove.Domain.ViewModels
{
    public class LoginUser : ILoginUser
    {
        /// <summary>
        /// Gets or sets the name of the user.
        /// </summary>
        /// <value>
        /// The name of the user.
        /// </value>
        [Required]
        [Display(ResourceType = typeof (ViewLabels), Name = "UserNameLabel")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [Required]
        [StringLength(100, ErrorMessageResourceType = typeof (Messages), ErrorMessageResourceName = "PasswordErrorMessage", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof (ViewLabels), Name = "PasswordLabel")]
        public string Password { get; set; }

        ///// <summary>
        ///// Gets or sets the old password.
        ///// </summary>
        ///// <value>
        ///// The old password.
        ///// </value>
        //[Required]
        //[DataType(DataType.Password)]
        //[Display(ResourceType = typeof (ViewLabels), Name = "CurrentPasswordLabel")]
        //public string OldPassword { get; set; }

        ///// <summary>
        ///// Gets or sets the new password.
        ///// </summary>
        ///// <value>
        ///// The new password.
        ///// </value>
        //[Required]
        //[StringLength(100, ErrorMessageResourceType = typeof (Messages), ErrorMessageResourceName = "PasswordErrorMessage", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(ResourceType = typeof (ViewLabels), Name = "NewPasswordLabel")]
        //public string NewPassword { get; set; }

        ///// <summary>
        ///// Gets or sets the confirm password.
        ///// </summary>
        ///// <value>
        ///// The confirm password.
        ///// </value>
        //[DataType(DataType.Password)]
        //[Display(ResourceType = typeof (ViewLabels), Name = "ConfirmPasswordLabel")]
        //[Compare("Password", ErrorMessageResourceType = typeof (Messages), ErrorMessageResourceName = "ConfirmPasswordErrorMessage")]
        //public string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [remember me]; otherwise, <c>false</c>.
        /// </value>
        [Display(ResourceType = typeof (ViewLabels), Name = "RememberMeLabel")]
        public bool RememberMe { get; set; }
    }
}