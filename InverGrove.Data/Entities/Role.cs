using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InverGrove.Data.Entities
{
    [Table("Role")]
    public class Role
    {
        public Role()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.UserRoles = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }

        [Required]
        [StringLength(64)]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}