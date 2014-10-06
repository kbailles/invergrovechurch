namespace InverGrove.Domain.Interfaces
{
    public interface ILoginUser
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

        ///// <summary>
        ///// Gets or sets the old password.
        ///// </summary>
        ///// <value>
        ///// The old password.
        ///// </value>
        //string OldPassword { get; set; }

        ///// <summary>
        ///// Gets or sets the new password.
        ///// </summary>
        ///// <value>
        ///// The new password.
        ///// </value>
        //string NewPassword { get; set; }

        ///// <summary>
        ///// Gets or sets the confirm password.
        ///// </summary>
        ///// <value>
        ///// The confirm password.
        ///// </value>
        //string ConfirmPassword { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remember me].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [remember me]; otherwise, <c>false</c>.
        /// </value>
        bool RememberMe { get; set; }
    }
}