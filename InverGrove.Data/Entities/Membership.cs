using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InverGrove.Data.Entities
{
    [Table("Membership")]
    public class Membership
    {
        public int MembershipId { get; set; }

        public int UserId { get; set; }

        public DateTime? DateLastLogin { get; set; }

        public DateTime? DateLockedOut { get; set; }

        public int FailedPasswordAnswerAttemptCount { get; set; }

        public DateTime FailedPasswordAnswerAttemptWindowStart { get; set; }

        public int FailedPasswordAttemptCount { get; set; }

        public DateTime FailedPasswordAttemptWindowStart { get; set; }

        public bool IsLockedOut { get; set; }

        [Required]
        [StringLength(128)]
        public string Password { get; set; }

        [Required]
        [StringLength(128)]
        public string PasswordSalt { get; set; }

        public byte PasswordFormat { get; set; }

        [Required]
        [StringLength(64)]
        public string PasswordQuestion { get; set; }

        [Required]
        [StringLength(64)]
        public string PasswordAnswer { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public DateTime? DateLastActivity { get; set; }

        public bool IsApproved { get; set; }

        public virtual PasswordFormat PasswordFormat1 { get; set; }

        public virtual User User { get; set; }
    }
}