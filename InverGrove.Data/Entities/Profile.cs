using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InverGrove.Data.Entities
{
    [Table("Profile")]
    public class Profile
    {
        public int ProfileId { get; set; }

        public int UserId { get; set; }

        public bool ReceiveEmailNotification { get; set; }

        public int PersonId { get; set; }

        public bool IsBaptized { get; set; }

        public bool IsLocal { get; set; }

        public bool IsActive { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsValidated { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual Person Person { get; set; }

        public virtual User User { get; set; }
    }
}