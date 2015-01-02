using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;
using InverGrove.Domain.Enums;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Helpers;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Resources;
using InverGrove.Domain.Services;
using InverGrove.Domain.Utils;
using Membership = InverGrove.Domain.Models.Membership;

namespace InverGrove.Domain.Providers
{
    public class MembershipProvider : System.Web.Security.MembershipProvider, IMembershipProvider
    {
        private readonly object syncRoot = new object();
        private string applicationName;
        private int commandTimeout;
        private string description;
        private bool enablePasswordReset;
        private bool enablePasswordRetrieval;
        private bool initialized;
        private int maxInvalidPasswordAttempts;
        private int minRequiredNonalphanumericCharacters;
        private int minRequiredNumericCharacters;
        private int minRequiredPasswordLength;
        private string providerName;
        private int passwordAttemptWindow;
        private int passwordFormat;
        private Regex passwordRegex;
        private string passwordStrengthRegularExpression;
        private bool requiresQuestionAndAnswer;
        private bool requiresUniqueEmail;
        private readonly bool convertToHash;
        private int maxPasswordLength;

        private readonly IMembershipService membershipService;
        private readonly IUserService userService;

        private const string MembershipProviderName = "InverGroveMembershipProvider";

        public MembershipProvider()
            : this(null, null)
        {
            // here because the provider can't call the constructor with optional parameters
        }

        public MembershipProvider(IMembershipService membershipService, IUserService userService)
        {
            this.convertToHash = bool.Parse(ConfigurationManager.AppSettings["ConvertToHash"]);
            this.userService = userService ?? UserService.Create();
            this.membershipService = membershipService ?? MembershipService.Create();
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow members to retrieve their passwords.
        /// </summary>
        /// <returns>true if the membership provider is configured to support password retrieval; otherwise, false. The default is false.</returns>
        public override bool EnablePasswordRetrieval
        {
            get { return this.enablePasswordRetrieval; }
        }

        /// <summary>
        /// Indicates whether the membership provider is configured to allow members to reset their passwords.
        /// </summary>
        /// <returns>true if the membership provider supports password reset; otherwise, false. The default is true.</returns>
        public override bool EnablePasswordReset
        {
            get { return this.enablePasswordReset; }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require the user to answer 
        /// a password question for password reset and retrieval.
        /// </summary>
        /// <returns>true if a password answer is required for password reset and retrieval; otherwise, false. 
        /// The default is true.</returns>
        public override bool RequiresQuestionAndAnswer
        {
            get { return this.requiresQuestionAndAnswer; }
        }

        /// <summary>
        /// The name of the application using the custom membership provider.
        /// </summary>
        /// <returns>The name of the application using the custom membership provider.</returns>
        public override string ApplicationName
        {
            get { return this.initialized ? this.applicationName : string.Empty; }
            set { this.applicationName = value; }
        }

        /// <summary>
        /// Gets the name of the provider.
        /// </summary>
        /// <value>
        /// The name of the provider.
        /// </value>
        public string ProviderName
        {
            get { return this.providerName; }
        }

        /// <summary>
        /// Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.
        /// </summary>
        /// <returns>The number of invalid password or password-answer attempts allowed before the membership user is locked out.</returns>
        public override int MaxInvalidPasswordAttempts
        {
            get { return this.maxInvalidPasswordAttempts; }
        }

        /// <summary>
        /// Gets the number of minutes in which a maximum number of invalid password or password-answer attempts are 
        /// allowed before the membership user is locked out.
        /// </summary>
        /// <returns>The number of minutes in which a maximum number of invalid password or password-answer attempts 
        /// are allowed before the membership user is locked out.</returns>
        public override int PasswordAttemptWindow
        {
            get { return this.passwordAttemptWindow; }
        }

        /// <summary>
        /// Gets a value indicating whether the membership provider is configured to require a unique e-mail address for each user name.
        /// </summary>
        /// <returns>true if the membership provider requires a unique e-mail address; otherwise, false. The default is true.</returns>
        public override bool RequiresUniqueEmail
        {
            get { return this.requiresUniqueEmail; }
        }

        /// <summary>
        /// Gets a value indicating the format for storing passwords in the membership data store.
        /// </summary>
        /// <returns>One of the <see cref="T:System.Web.Security.MembershipPasswordFormat"/> values indicating the 
        /// format for storing passwords in the data store.</returns>
        public override MembershipPasswordFormat PasswordFormat
        {
            get { return (MembershipPasswordFormat)this.passwordFormat; }
        }

        /// <summary>
        /// Gets the minimum length required for a password.
        /// </summary>
        /// <returns>The minimum length required for a password. </returns>
        public override int MinRequiredPasswordLength
        {
            get { return this.minRequiredPasswordLength; }
        }

        /// <summary>
        /// Gets the minimum number of special characters that must be present in a valid password.
        /// </summary>
        /// <returns>The minimum number of special characters that must be present in a valid password.</returns>
        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return this.minRequiredNonalphanumericCharacters; }
        }

        /// <summary>
        /// Gets the min required numeric characters.
        /// </summary>
        public int MinRequiredNumericCharacters
        {
            get { return this.minRequiredNumericCharacters; }
        }

        /// <summary>
        /// Gets the regular expression used to evaluate a password.
        /// </summary>
        /// <returns>A regular expression used to evaluate a password.</returns>
        public override string PasswordStrengthRegularExpression
        {
            get { return this.passwordStrengthRegularExpression; }
        }

        /// <summary>
        /// Gets the initialization message.
        /// </summary>
        public string InitializationMessage { get; private set; }

        /// <summary>
        /// Gets the command timeout.
        /// </summary>
        public int CommandTimeout
        {
            get { return this.commandTimeout; }
        }

        /// <summary>
        /// Gets the length of the max password.
        /// </summary>
        /// <value>
        /// The length of the max password.
        /// </value>
        public int MaxPasswordLength
        {
            get { return this.maxPasswordLength; }
        }

        /// <summary>
        /// Processes a request to update the password for a membership user.
        /// </summary>
        /// <param name="username">The user to update the password for.</param>
        /// <param name="oldPassword">The current password for the specified user.</param>
        /// <param name="newPassword">The new password for the specified user.</param>
        /// <returns>
        /// true if the password was updated successfully; otherwise, false.
        /// </returns>
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ParameterNullException("username");
            }

            if (string.IsNullOrEmpty(oldPassword))
            {
                throw new ParameterNullException("oldPassword");
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ParameterNullException("newPassword");
            }

            this.CheckNewPasswordValidity(oldPassword, newPassword);

            var membership = this.GetMembershipData(username: username);

            if (AccountHelper.ValidateMembershipPassword(oldPassword, membership))
            {
                string salt = string.Empty;

                if (this.convertToHash)
                {
                    membership.PasswordFormatId = (int)InverGrovePasswordFormat.Hashed;
                    salt = salt.GetRandomSalt();
                }
                else
                {
                    if (membership.PasswordFormatId == (int)InverGrovePasswordFormat.Hashed)
                    {
                        salt = membership.PasswordSalt;
                    }
                    else
                    {
                        membership.PasswordFormatId = (int)InverGrovePasswordFormat.Clear;
                    }
                }

                membership.PasswordSalt = salt;

                // use existing password format from database.
                membership.Password = newPassword.FormatPasscode((InverGrovePasswordFormat)membership.PasswordFormatId, membership.PasswordSalt);

                return this.UpdateMembershipData(membership);
            }

            return false;

        }

        /// <summary>
        /// Resets a user's password to a new, automatically generated password.
        /// </summary>
        /// <param name="username">The user to reset the password for.</param>
        /// <param name="answer">The password answer for the specified user.</param>
        /// <returns>
        /// The new password for the specified user.
        /// </returns>
        public override string ResetPassword(string username, string answer)
        {
            if (!this.EnablePasswordReset)
            {
                throw new NotSupportedException(Messages.NotConfiguredToSupportPasswordResets);
            }

            var membership = this.membershipService.GetMembershipByUserName(username);

            return this.GetGeneratedPassword((Membership)membership);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <returns></returns>
        /// <exception cref="System.NotSupportedException"></exception>
        public string ResetPassword(IMembership membership)
        {
            if (!this.EnablePasswordReset)
            {
                throw new NotSupportedException(Messages.NotConfiguredToSupportPasswordResets);
            }

            return this.GetGeneratedPassword((Membership)membership);
        }

        /// <summary>
        /// Resets the password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="securityAnswer">The security answer.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        public bool ResetPassword(string username, string securityAnswer, string newPassword)
        {
            if (!this.EnablePasswordReset)
            {
                throw new NotSupportedException(Messages.NotConfiguredToSupportPasswordResets);
            }

            if (string.IsNullOrEmpty(securityAnswer))
            {
                throw new ParameterNullException("securityAnswer");
            }

            if (string.IsNullOrEmpty(newPassword))
            {
                throw new ParameterNullException("newPassword");
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ParameterNullException("username");
            }

            if (!this.passwordRegex.IsMatch(newPassword))
            {
                return false;
            }

            var membership = this.GetMembershipData(username: username);

            var strippedSecurityAnswer = securityAnswer.ToSecurityAnswer();
            var hashedSecurityAnswer = strippedSecurityAnswer.FormatPasscode(
                (InverGrovePasswordFormat)membership.PasswordFormatId, membership.PasswordSalt);

            if (!membership.PasswordAnswer.Equals(hashedSecurityAnswer))
            {
                if (!membership.PasswordAnswer.IsValidHashValue())
                {
                    throw new ProviderException(Messages.SecurityAnswerIncorrect);
                }

                if (!membership.PasswordAnswer.Equals(securityAnswer))
                {
                    throw new ProviderException(Messages.SecurityAnswerIncorrect);
                }
            }

            return this.UpdatePasswordAndAnswer(newPassword, membership, hashedSecurityAnswer);
        }

        /// <summary>
        /// Gets the name of the account by user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public IMembership GetAccountByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ParameterNullException("userName");
            }

            return this.membershipService.GetMembershipByUserName(userName);
        }

        /// <summary>
        /// Updates the security question answer.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="securityQuestion">The security question.</param>
        /// <param name="securityAnswer">The security answer.</param>
        /// <returns></returns>
        public bool UpdateSecurityQuestionAnswer(string userName, string securityQuestion, string securityAnswer)
        {
            if (string.IsNullOrEmpty(securityQuestion))
            {
                throw new ParameterNullException("securityQuestion");
            }
            if (string.IsNullOrEmpty(securityAnswer))
            {
                throw new ParameterNullException("securityAnswer");
            }

            var strippedSecurityAnswer = securityAnswer.ToSecurityAnswer();

            if (string.IsNullOrEmpty(strippedSecurityAnswer))
            {
                throw new ApplicationException(string.Format("Security answer is invalid. {0}", securityAnswer));
            }

            var membership = this.GetMembershipData(username: userName);

            var hashedSecurityAnswer = strippedSecurityAnswer.FormatPasscode(
                (InverGrovePasswordFormat)membership.PasswordFormatId, membership.PasswordSalt);

            membership.PasswordAnswer = ((membership.PasswordFormatId == (int)InverGrovePasswordFormat.Hashed) ||
                this.convertToHash) ? hashedSecurityAnswer : strippedSecurityAnswer;
            membership.PasswordQuestion = securityQuestion;

            return this.UpdateMembershipData(membership);
        }

        /// <summary>
        /// Creates the user account.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="email">The email.</param>
        /// <param name="passwordQuestion">The password question.</param>
        /// <param name="passwordAnswer">The password answer.</param>
        /// <returns></returns>
        public IMembership CreateUserAccount(string username, string password, string email,
            string passwordQuestion, string passwordAnswer)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ApplicationException("User name is empty.");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ApplicationException("Password is empty.");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new ApplicationException("Email is empty.");
            }
            if (string.IsNullOrEmpty(passwordQuestion))
            {
                throw new ApplicationException("Password question is empty.");
            }
            if (string.IsNullOrEmpty(passwordAnswer))
            {
                throw new ApplicationException("Password answer is empty.");
            }

            MembershipCreateStatus status;
            var membershipUser = this.CreateUser(username, password, email, passwordQuestion, passwordAnswer,
                false, null, out status);

            if ((status != MembershipCreateStatus.Success) || (membershipUser == null))
            {
                return null;
            }

            var membership = this.GetMembershipData(username: username);

            membership.DateModified = DateTime.Now;

            this.membershipService.UpdateMembership(membership);

            return membership;
        }

        /// <summary>
        /// Verifies that the specified user name and password exist in the data source.
        /// </summary>
        /// <param name="username">The name of the user to validate.</param>
        /// <param name="password">The password for the specified user.</param>
        /// <returns>
        /// true if the specified username and password are valid; otherwise, false.
        /// </returns>
        public override bool ValidateUser(string username, string password)
        {
            Guard.ParameterNotNullOrEmpty("username", username);
            Guard.ParameterNotNullOrEmpty("password", password);

            var account = this.GetMembershipData(username: username);

            var result = AccountHelper.ValidateMembershipPassword(password, account);

            if (result)
            {
                this.UpdateLastLoginData(account);
            }

            return result;
        }

        /// <summary>
        /// Validates the password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="status">The status.</param>
        /// <returns></returns>
        public bool ValidatePassword(string username, string password, out string status)
        {
            // cant test this, using membership API
            status = "";
            var e = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(e);

            if (e.Cancel)
            {
                status = e.FailureInformation == null ? Messages.MembershipCustomPasswordValidationFailure : e.FailureInformation.Message;

                return false;
            }

            return true;
        }

        /// <summary>
        /// Adds a new membership user to the data source.
        /// </summary>
        /// <param name="username">The user name for the new user.</param>
        /// <param name="password">The password for the new user.</param>
        /// <param name="email">The e-mail address for the new user.</param>
        /// <param name="passwordQuestion">The password question for the new user.</param>
        /// <param name="passwordAnswer">The password answer for the new user</param>
        /// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
        /// <param name="providerUserKey">The unique identifier from the membership data source for the user.</param>
        /// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus"/>
        ///  enumeration value indicating whether the user was created successfully.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the information for the newly created user.
        /// </returns>
        public override MembershipUser CreateUser(string username, string password,
            string email, string passwordQuestion, string passwordAnswer,
            bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            status = MembershipCreateStatus.ProviderError;

            if (string.IsNullOrEmpty(username))
            {
                throw new ApplicationException("User name is empty.");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ApplicationException("Password is empty.");
            }
            // email is attached to the Person
            //if (string.IsNullOrEmpty(email))
            //{
            //    throw new ApplicationException("Email is empty.");
            //}
            if (string.IsNullOrEmpty(passwordQuestion))
            {
                throw new ApplicationException("passwordQuestion question is empty.");
            }
            if (string.IsNullOrEmpty(passwordAnswer))
            {
                throw new ApplicationException("passwordAnswer answer is empty.");
            }

            var membership = this.membershipService.CreateMembershipUser(username, password, email,
                passwordQuestion, passwordAnswer, isApproved, this.PasswordFormat);

            MembershipUser membershipUser = null;

            if (membership != null)
            {
                membershipUser = this.GetUser(membership.UserId);
            }

            status = (membershipUser == null ? MembershipCreateStatus.ProviderError : MembershipCreateStatus.Success);

            return membershipUser;
        }

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
        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
            string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the password for the specified user name from the data source.
        /// </summary>
        /// <param name="username">The user to retrieve the password for.</param>
        /// <param name="answer">The password answer for the user.</param>
        /// <returns>
        /// The password for the specified user name.
        /// </returns>
        public override string GetPassword(string username, string answer)
        {
            if (!this.EnablePasswordRetrieval)
            {
                throw new NotSupportedException(Messages.MembershipPasswordRetrievalNotSupported);
            }

            string password = "";

            var account = this.GetMembershipData(username: username);

            if (account != null)
            {
                password = account.Password;
            }

            return password;
        }

        /// <summary>
        /// Updates information about a user in the data source.
        /// </summary>
        /// <param name="user">A <see cref="T:System.Web.Security.MembershipUser"/> object that represents 
        /// the user to update and the updated information for the user.</param>
        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Clears a lock so that the membership user can be validated.
        /// </summary>
        /// <param name="userName">The membership user whose lock status you want to clear.</param>
        /// <returns>
        /// true if the membership user was successfully unlocked; otherwise, false.
        /// </returns>
        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets user information from the data source based on the unique identifier for the membership user. Provides 
        /// an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <param name="providerUserKey">The unique identifier for the membership user to get information for.</param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user 
        /// information without updating the last-activity date/time stamp for the user.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
        /// </returns>
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            if (providerUserKey == null)
            {
                throw new ParameterNullException("providerUserKey");
            }

            return this.GetUser(providerUserKey.ToString(), userIsOnline);
        }

        /// <summary>
        /// Gets information from the data source for a user. Provides an option to update the last-activity date/time stamp for the user.
        /// </summary>
        /// <param name="username">The name of the user to get information for.</param>
        /// <param name="userIsOnline">true to update the last-activity date/time stamp for the user; false to return user 
        /// information without updating the last-activity date/time stamp for the user.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUser"/> object populated with the specified user's information from the data source.
        /// </returns>
        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ParameterNullException("username");
            }

            var membership = this.membershipService.GetMembershipByUserName(username);

            if (membership != null)
            {
                if (userIsOnline)
                {
                    this.UpdateLastLoginData((Membership)membership);
                }

                return membership.ToMembershipUser(username);
            }

            return null;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public MembershipUser GetUser(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException("id");
            }

            var membership = this.membershipService.GetMembershipByUserId(id);
            var user = this.userService.GetUser(id);

            if ((membership != null) && (user != null))
            {
                return membership.ToMembershipUser(user.UserName);
            }

            return null;
        }

        /// <summary>
        /// Gets the user name associated with the specified e-mail address.
        /// </summary>
        /// <param name="email">The e-mail address to search for.</param>
        /// <returns>
        /// The user name associated with the specified e-mail address. If no match is found, return null.
        /// </returns>
        public override string GetUserNameByEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ParameterNullException("email");
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes a user from the membership data source.
        /// </summary>
        /// <param name="username">The name of the user to delete.</param>
        /// <param name="deleteAllRelatedData">true to delete data related to the user from the database; false to leave 
        /// data related to the user in the database.</param>
        /// <returns>
        /// true if the user was successfully deleted; otherwise, false.
        /// </returns>
        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ParameterNullException("username");
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of all the members in the data source in pages of data.
        /// </summary>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched members.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of 
        /// <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the
        ///  page specified by <paramref name="pageIndex"/>.
        /// </returns>
        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var membershipUsers = this.membershipService.GetAllMembershipUsers().ToSafeList();
            totalRecords = membershipUsers.Count;
            var pagedMembershipUsers = membershipUsers.Skip(pageIndex*pageSize).Take(pageSize);
            MembershipUserCollection membershipUserCollection = new MembershipUserCollection();

            foreach (var membershipUser in pagedMembershipUsers)
            {
                membershipUserCollection.Add(membershipUser.ToMembershipUser(membershipUser.User.UserName));
            }

            return membershipUserCollection;
        }

        /// <summary>
        /// Gets the number of members currently accessing the application.
        /// </summary>
        /// <returns>
        /// The number of members currently accessing the application.
        /// </returns>
        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of membership members where the user name contains the specified user name to match.
        /// </summary>
        /// <param name="usernameToMatch">The user name to search for.</param>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched members.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of 
        /// <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at the
        ///  page specified by <paramref name="pageIndex"/>.
        /// </returns>
        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets a collection of membership members where the e-mail address contains the specified e-mail address to match.
        /// </summary>
        /// <param name="emailToMatch">The e-mail address to search for.</param>
        /// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex"/> is zero-based.</param>
        /// <param name="pageSize">The size of the page of results to return.</param>
        /// <param name="totalRecords">The total number of matched members.</param>
        /// <returns>
        /// A <see cref="T:System.Web.Security.MembershipUserCollection"/> collection that contains a page of
        /// <paramref name="pageSize"/><see cref="T:System.Web.Security.MembershipUser"/> objects beginning at
        ///  the page specified by <paramref name="pageIndex"/>.
        /// </returns>
        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Initializes the provider.
        /// </summary>
        /// <param name="name">The friendly name of the provider.</param>
        /// <param name="config">A collection of the name/value pairs representing the provider-specific
        ///  attributes specified in the configuration for this provider.</param>
        /// <exception cref="T:System.ArgumentNullException">The name of the provider is null.</exception>
        /// <exception cref="T:System.ArgumentException">The name of the provider has a length of zero.</exception>
        /// <exception cref="T:System.InvalidOperationException">An attempt is made to call
        ///  <see cref="M:System.Configuration.Provider.ProviderBase.Initialize(System.String,System.Collections.Specialized.NameValueCollection)"/>
        /// on a provider after the provider has already been initialized.</exception>
        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
            {
                throw new ParameterNullException("config");
            }

            if (string.IsNullOrEmpty(name))
            {
                name = MembershipProviderName;
            }

            this.providerName = name;

            if (this.initialized)
            {
                return;
            }

            this.HandleApplicationName(config);

            lock (this.syncRoot)
            {
                this.description = config["description"];

                if (string.IsNullOrEmpty(this.description))
                {
                    config.Remove("description");
                    config.Add("description", "Rest based membership provider.");
                    this.description = config["description"];
                }

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} initializing.\r\n", this.description);

                int i;

                base.Initialize(name, config);

                this.SetMembershipValuesByConfigValues(config, sb);

                this.SetPasswordStrengthValues(sb);

                this.commandTimeout = int.TryParse(config["commandTimeout"], out i) ? i : 20000;

                this.SetPasswordFormatValues(config);

                this.initialized = true;

                sb.AppendFormat("Password format: {0}\r\n", this.passwordFormat);

                this.InitializationMessage = sb.ToString();
            }
        }

        private Membership GetMembershipData(int? userId = null, string username = null)
        {
            IMembership membership;

            if ((userId != null) && (userId > 0))
            {
                membership = this.membershipService.GetMembershipByUserId((int)userId);
            }
            else
            {
                membership = this.membershipService.GetMembershipByUserName(username);
            }

            return (Membership)membership;
        }

        private void UpdateLastLoginData(Membership membership)
        {
            var user = this.userService.GetUser(membership.UserId);
            user.LastActivityDate = DateTime.Now;

            this.UpdateUserData((User)user);

            membership.DateLastActivity = DateTime.Now;
            membership.DateLastLogin = DateTime.Now;

            this.UpdateMembershipData(membership);
        }

        private bool UpdatePasswordAndAnswer(string newPassword, Membership membership, string hashedSecurityAnswer)
        {
            string salt = string.Empty;

            if (this.convertToHash)
            {
                membership.PasswordAnswer = hashedSecurityAnswer;
                membership.PasswordFormatId = (int)InverGrovePasswordFormat.Hashed;
                salt = salt.GetRandomSalt();
            }
            else
            {
                if (membership.PasswordFormatId == (int)InverGrovePasswordFormat.Hashed)
                {
                    salt = membership.PasswordSalt;
                }
                else
                {
                    membership.PasswordFormatId = (int)InverGrovePasswordFormat.Clear;
                }
            }

            membership.PasswordSalt = salt;
            membership.Password = newPassword.FormatPasscode((InverGrovePasswordFormat)membership.PasswordFormatId, membership.PasswordSalt);

            return this.UpdateMembershipData(membership);
        }

        private void CheckNewPasswordValidity(string oldPassword, string newPassword)
        {
            var errorMessage = new StringBuilder();

            if (oldPassword == newPassword)
            {
                errorMessage.Append(Messages.ChangePasswordSameWord).Append("\r\n");
            }

            if (newPassword.Length < this.minRequiredPasswordLength)
            {
                errorMessage.Append(string.Format(Messages.ChangePasswordDoesNotMeetRequirements,
                    this.minRequiredPasswordLength)).Append("\r\n");
            }

            int nonAlphanumericCount = newPassword.Where((t, i) => !char.IsLetterOrDigit(newPassword, i)).Count();

            if (nonAlphanumericCount < this.MinRequiredNonAlphanumericCharacters)
            {
                errorMessage.Append(string.Format(Messages.ChangePasswordDoesNotContainSymbol,
                    this.MinRequiredNonAlphanumericCharacters)).Append("\r\n");
            }

            if (newPassword.Length > this.MaxPasswordLength)
            {
                errorMessage.Append(string.Format(Messages.ParameterTooLong, "newPassword",
                    this.MaxPasswordLength.ToString(CultureInfo.InvariantCulture))).Append("\r\n");
            }

            if (errorMessage.Length > 0)
            {
                throw new ProviderException(errorMessage.ToString());
            }
        }

        private bool UpdateMembershipData(Membership membership)
        {
            return this.membershipService.UpdateMembership(membership);
        }

        private void UpdateUserData(User user)
        {
            this.userService.UpdateUser(user);
        }

        private void SetMembershipValuesByConfigValues(NameValueCollection config, StringBuilder sb)
        {
            bool b;
            int i;
            bool.TryParse(config["enablePasswordRetrieval"], out b);
            sb.AppendFormat("Enable password retrieval: {0}\r\n", b);
            this.enablePasswordRetrieval = b;

            bool.TryParse(config["enablePasswordReset"], out b);
            sb.AppendFormat("Enable password reset: {0}\r\n", b);
            this.enablePasswordReset = b;

            bool.TryParse(config["requiresQuestionAndAnswer"], out b);
            sb.AppendFormat("Requires question and answer: {0}\r\n", b);
            this.requiresQuestionAndAnswer = b;

            bool.TryParse(config["requiresUniqueEmail"], out b);
            sb.AppendFormat("Requires unique email: {0}\r\n", b);
            this.requiresUniqueEmail = b;

            int.TryParse(config["maxInvalidPasswordAttempts"], out i);
            sb.AppendFormat("Max invalid password attempts: {0}\r\n", i);
            this.maxInvalidPasswordAttempts = i;

            int.TryParse(config["passwordAttemptWindow"], out i);
            sb.AppendFormat("Password attempt window: {0}\r\n", i);
            this.passwordAttemptWindow = i;

            int.TryParse(config["minRequiredPasswordLength"], out i);
            sb.AppendFormat("Minimum required password lengths: {0}\r\n", i);
            this.minRequiredPasswordLength = i;

            int.TryParse(config["minRequiredNonalphanumericCharacters"], out i);
            sb.AppendFormat("Minimum required non-alphanumeric characters: {0}\r\n", i);
            this.minRequiredNonalphanumericCharacters = i;

            int.TryParse(config["minRequiredNumericCharacters"], out i);
            sb.AppendFormat("Minimum required numeric characters: {0}\r\n", i);
            this.minRequiredNumericCharacters = i;

            int.TryParse(ConfigurationManager.AppSettings["MaximumPasswordLength"], out i);
            sb.AppendFormat("Maximum password length: {0}\r\n", i);
            this.maxPasswordLength = i;

            this.passwordStrengthRegularExpression = config["passwordStrengthRegularExpression"];
        }

        private void SetPasswordStrengthValues(StringBuilder sb)
        {
            if (!string.IsNullOrEmpty(this.passwordStrengthRegularExpression))
            {
                this.passwordStrengthRegularExpression = this.passwordStrengthRegularExpression.Trim();
                this.passwordRegex = new Regex(this.passwordStrengthRegularExpression);

                try
                {
                    if (this.minRequiredNonalphanumericCharacters > this.minRequiredPasswordLength)
                    {
                        throw new ProviderException(Messages.CanNotBeMoreThanMinRequiredPasswordLength);
                    }
                }
                catch (ArgumentException exception)
                {
                    throw new ProviderException(exception.Message, exception);
                }
            }
            else
            {
                if (this.minRequiredNonalphanumericCharacters > this.minRequiredPasswordLength)
                {
                    throw new ProviderException(Messages.CanNotBeMoreThanMinRequiredPasswordLength);
                }
                this.passwordStrengthRegularExpression = string.Empty;
            }
            sb.AppendFormat(Messages.PasswordRegexStrength, this.passwordStrengthRegularExpression);

            if (this.minRequiredNonalphanumericCharacters > this.minRequiredPasswordLength)
            {
                throw new ProviderException(Messages.CanNotBeMoreThanMinRequiredPasswordLength);
            }
        }

        private void SetPasswordFormatValues(NameValueCollection config)
        {
            // hashed is the default if not filled out.
            string str = config["passwordFormat"] ?? "HASHED";
            str = str.ToUpperInvariant();

            if (str != "CLEAR")
            {
                if (str == "ENCRYPTED")
                {
                    this.passwordFormat = (int)MembershipPasswordFormat.Encrypted;
                }
                if (str == "HASHED")
                {
                    if (this.EnablePasswordRetrieval)
                    {
                        throw new ProviderException(Messages.CannotRetrieveHashedPassword);
                    }
                    this.passwordFormat = (int)MembershipPasswordFormat.Hashed;
                }
            }
            else
            {
                this.passwordFormat = (int)MembershipPasswordFormat.Clear;
            }
        }

        private void HandleApplicationName(NameValueCollection config)
        {
            this.applicationName = config["applicationName"];

            if (string.IsNullOrEmpty(this.applicationName))
            {
                throw new ApplicationException(Messages.InvalidApplicationName);
            }
            if (this.applicationName.Length > 256)
            {
                throw new ProviderException(Messages.ApplicationNameTooLong);
            }
        }

        private string GetGeneratedPassword(Membership membership)
        {
            if (membership == null)
            {
                return string.Empty;
            }

            string newPassword = GeneratePassword(this.MinRequiredPasswordLength, this.MinRequiredNonAlphanumericCharacters);

            if (this.convertToHash)
            {
                string salt = string.Empty;
                membership.PasswordFormatId = (int)InverGrovePasswordFormat.Hashed;
                membership.PasswordSalt = salt.GetRandomSalt();
            }

            membership.Password = newPassword.FormatPasscode((InverGrovePasswordFormat)membership.PasswordFormatId, membership.PasswordSalt);

            this.UpdateMembershipData(membership);

            return newPassword;
        }

        /// <summary>
        ///     Generates the password.
        /// </summary>
        /// <param name = "length">The length.</param>
        /// <param name = "numberOfNonAlphanumericCharacters">The number of non alphanumeric characters.</param>
        /// <returns></returns>
        internal static string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
        {
            char[] punctuations = "!@@$%^^*()_-+=[{]};:>|./?".ToCharArray();

            if ((length < 1) || (length > 128))
            {
                throw new ArgumentException(string.Format(Messages.MembershipPasswordLengthIncorrect, length), "length");
            }
            if ((numberOfNonAlphanumericCharacters > length) || (numberOfNonAlphanumericCharacters < 0))
            {
                throw new ArgumentException(
                string.Format(Messages.MembershipMinRequiredNonAlphanumericCharactersIncorrect, numberOfNonAlphanumericCharacters,
                    length), "numberOfNonAlphanumericCharacters");
            }

            byte[] data = new byte[length];
            char[] chArray = new char[length];
            int nonAlphanumericCount = 0;
            new RNGCryptoServiceProvider().GetBytes(data);

            for (int i = 0; i < length; i++)
            {
                int num4 = data[i] % 0x57;
                if (num4 < 10)
                {
                    chArray[i] = (char)(0x30 + num4);
                }
                else if (num4 < 0x24)
                {
                    chArray[i] = (char)((0x41 + num4) - 10);
                }
                else if (num4 < 0x3e)
                {
                    chArray[i] = (char)((0x61 + num4) - 0x24);
                }
                else
                {
                    chArray[i] = punctuations[num4 - 0x3e];
                    nonAlphanumericCount++;
                }
            }

            if (nonAlphanumericCount < numberOfNonAlphanumericCharacters)
            {
                Random random = new Random();
                for (int j = 0; j < (numberOfNonAlphanumericCharacters - nonAlphanumericCount); j++)
                {
                    int num6;
                    do
                    {
                        num6 = random.Next(0, length);
                    } while (!char.IsLetterOrDigit(chArray[num6]));
                    chArray[num6] = punctuations[random.Next(0, punctuations.Length)];
                }
            }
            return new string(chArray);
        }
    }
}