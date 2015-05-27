using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InverGrove.Data.Entities
{
    [Table("User")]
    public class User
    {
        public User()
        {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
            this.MemberNotes = new HashSet<MemberNote>();
            this.Memberships = new HashSet<Membership>();
            this.Profiles = new HashSet<Profile>();
            this.UserRoles = new HashSet<UserRole>();
            this.Responsibilities = new HashSet<Responsibility>();
            this.Sermons = new HashSet<Sermon>();
        }

        public int UserId { get; set; }

        public DateTime LastActivityDate { get; set; }

        public bool IsAnonymous { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual ICollection<MemberNote> MemberNotes { get; set; }

        public virtual ICollection<Membership> Memberships { get; set; }

        public virtual ICollection<Profile> Profiles { get; set; }

        public virtual ICollection<Responsibility> Responsibilities { get; set; }

        public virtual ICollection<Sermon> Sermons { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}