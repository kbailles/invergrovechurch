using System.Web.Security;

namespace InverGrove.Domain.Interfaces
{
    public interface IMembershipProvider
    {
        /// <summary>
        /// Indicates whether the membership provider is configured to allow users to reset their passwords.
        /// </summary>
        /// <returns>true if the membership provider supports password reset; otherwise, false. The default is true.</returns>
        bool EnablePasswordReset { get; }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require the user to answer 
        /// a password question for password reset and retrieval.
        /// </summary>
        /// <returns>true if a password answer is required for password reset and retrieval; otherwise, false. 
        /// The default is true.</returns>
        bool RequiresQuestionAndAnswer { get; }

        /// <summary>
        /// Processes a request to update the password for a membership user.
        /// </summary>
        /// <param name="username">The user to update the password for.</param>
        /// <param name="oldPassword">The current password for the specified user.</param>
        /// <param name="newPassword">The new password for the specified user.</param>
        /// <returns>
        /// true if the password was updated successfully; otherwise, false.
        /// </returns>
        bool ChangePassword(string username, string oldPassword, string newPassword);

        /// <summary>
        /// Resets a user's password to a new, automatically generated password.
        /// </summary>
        /// <param name="username">The user to reset the password for.</param>
        /// <param name="answer">The password answer for the specified user.</param>
        /// <returns>
        /// The new password for the specified user.
        /// </returns>
        string ResetPassword(string username, string answer);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException"></exception>
        string ResetPassword(IMembership membership);

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="securityAnswer">The security answer.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        bool ResetPassword(string username, string securityAnswer, string newPassword);

        /// <summary>
        /// Gets the name of the account by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        IMembership GetAccountByUserName(string userName);

        /// <summary>
        /// Updates the security question answer.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="securityQuestion">The security question.</param>
        /// <param name="securityAnswer">The security answer.</param>
        /// <returns></returns>
        bool UpdateSecurityQuestionAnswer(string userName, string securityQuestion, string securityAnswer);

        /// <summary>
        /// Verifies that the specified user name and password exist in the data source.
        /// </summary>
        /// <param name="username">The name of the user to validate.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <returns>
        /// true if the specified username and password are valid; otherwise, false.
        /// </returns>
        bool ValidateUser(string username, string password);

        /// <summary>
        /// Validates the password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        bool ValidatePassword(string username, string password, out string status);

        /// <summary>
        /// Processes a request to update the password question and answer for a membership user.
        /// </summary>
        /// <param name="username">The user to change the password question and answer for.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <param name="newPasswordQuestion">The new password question for the specified user.</param>
        /// <param name="newPasswordAnswer">The new password answer for the specified user.</param>
        /// <returns>
        /// true if the password question and answer are updated successfully; otherwise, false.
        /// </returns>
        bool ChangePasswordQuestionAndAnswer(string username, string password,
            string newPasswordQuestion, string newPasswordAnswer);

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        MembershipUser GetUser(int id);
    }
}