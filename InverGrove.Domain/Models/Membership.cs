using System;
using InverGrove.Domain.Interfaces;
using Invergrove.Domain.Models;

namespace InverGrove.Domain.Models
{
    public class Membership : Resource, IMembership
    {
        /// <summary>
        /// Gets or sets the membership ID.
        /// </summary>
        /// <value>
        /// The membership ID.
        /// </value>
        public int MembershipId { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        /// <value>
        /// The user id.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the date last activity.
        /// </summary>
        /// <value>
        /// The date last activity.
        /// </value>
        public DateTime? DateLastActivity { get; set; }

        /// <summary>
        /// Gets or sets the date last login.
        /// </summary>
        /// <value>
        /// The date last login.
        /// </value>
        public DateTime? DateLastLogin { get; set; }

        /// <summary>
        /// Gets or sets the date locked out.
        /// </summary>
        /// <value>
        /// The date locked out.
        /// </value>
        public DateTime? DateLockedOut { get; set; }

        /// <summary>
        /// Gets or sets the failed password answer attempt count.
        /// </summary>
        /// <value>
        /// The failed password answer attempt count.
        /// </value>
        public int FailedPasswordAnswerAttemptCount { get; set; }

        /// <summary>
        /// Gets or sets the failed password answer attempt window start.
        /// </summary>
        /// <value>
        /// The failed password answer attempt window start.
        /// </value>
        public DateTime FailedPasswordAnswerAttemptWindowStart { get; set; }

        /// <summary>
        /// Gets or sets the failed password attempt count.
        /// </summary>
        /// <value>
        /// The failed password attempt count.
        /// </value>
        public int FailedPasswordAttemptCount { get; set; }

        /// <summary>
        /// Gets or sets the failed password attempt window start.
        /// </summary>
        /// <value>
        /// The failed password attempt window start.
        /// </value>
        public DateTime FailedPasswordAttemptWindowStart { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is locked out.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is locked out; otherwise, <c>false</c>.
        /// </value>
        public bool IsLockedOut { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is approved.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is approved; otherwise, <c>false</c>.
        /// </value>
        public bool IsApproved { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        /// <value>
        /// The password salt.
        /// </value>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the password format.
        /// </summary>
        /// <value>
        /// The password format.
        /// </value>
        public int PasswordFormatId { get; set; }

        /// <summary>
        /// Gets or sets the password question.
        /// </summary>
        /// <value>
        /// The password question.
        /// </value>
        public string PasswordQuestion { get; set; }

        /// <summary>
        /// Gets or sets the password answer.
        /// </summary>
        /// <value>
        /// The password answer.
        /// </value>
        public string PasswordAnswer { get; set; }

        /// <summary>
        /// Gets or sets the date created.
        /// </summary>
        /// <value>
        /// The date created.
        /// </value>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date modified.
        /// </summary>
        /// <value>
        /// The date modified.
        /// </value>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public IUser User { get; set; }
    }
}