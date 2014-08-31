using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InverGrove.Data.Entities
{
    [Table("PasswordFormat")]
    public class PasswordFormat
    {
        public PasswordFormat()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.Memberships = new HashSet<Membership>();
        }

        [Key]
        [Column("PasswordFormat")]
        public byte PasswordFormat1 { get; set; }

        [Required]
        [StringLength(64)]
        public string PasswordFormatDescription { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual ICollection<Membership> Memberships { get; set; }
    }
}