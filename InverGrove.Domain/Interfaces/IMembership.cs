using System;

namespace InverGrove.Domain.Interfaces
{
    public interface IMembership
    {
        /// <summary>
        /// Gets or sets the membership ID.
        /// </summary>
        /// <value>
        /// The membership ID.
        /// </value>
        int MembershipId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        int UserId { get; set; }

        /// <summary>
        /// Gets or sets the date last activity.
        /// </summary>
        /// <value>
        /// The date last activity.
        /// </value>
        DateTime? DateLastActivity { get; set; }

        /// <summary>
        /// Gets or sets the date last login.
        /// </summary>
        /// <value>
        /// The date last login.
        /// </value>
        DateTime? DateLastLogin { get; set; }

        /// <summary>
        /// Gets or sets the date locked out.
        /// </summary>
        /// <value>
        /// The date locked out.
        /// </value>
        DateTime? DateLockedOut { get; set; }

        /// <summary>
        /// Gets or sets the failed password answer attempt count.
        /// </summary>
        /// <value>
        /// The failed password answer attempt count.
        /// </value>
        int FailedPasswordAnswerAttemptCount { get; set; }

        /// <summary>
        /// Gets or sets the failed password answer attempt window start.
        /// </summary>
        /// <value>
        /// The failed password answer attempt window start.
        /// </value>
        DateTime FailedPasswordAnswerAttemptWindowStart { get; set; }

        /// <summary>
        /// Gets or sets the failed password attempt count.
        /// </summary>
        /// <value>
        /// The failed password attempt count.
        /// </value>
        int FailedPasswordAttemptCount { get; set; }

        /// <summary>
        /// Gets or sets the failed password attempt window start.
        /// </summary>
        /// <value>
        /// The failed password attempt window start.
        /// </value>
        DateTime FailedPasswordAttemptWindowStart { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is locked out.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is locked out; otherwise, <c>false</c>.
        /// </value>
        bool IsLockedOut { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is approved.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is approved; otherwise, <c>false</c>.
        /// </value>
        bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        string Password { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        /// <value>
        /// The password salt.
        /// </value>
        string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the password format.
        /// </summary>
        /// <value>
        /// The password format.
        /// </value>
        int PasswordFormatId { get; set; }

        /// <summary>
        /// Gets or sets the password question.
        /// </summary>
        /// <value>
        /// The password question.
        /// </value>
        string PasswordQuestion { get; set; }

        /// <summary>
        /// Gets or sets the password answer.
        /// </summary>
        /// <value>
        /// The password answer.
        /// </value>
        string PasswordAnswer { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>
        /// The date modified.
        /// </value>
        DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        IUser User { get; set; }
    }
}