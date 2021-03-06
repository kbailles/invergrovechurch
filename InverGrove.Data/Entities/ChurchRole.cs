﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InverGrove.Data.Entities
{
    [Table("ChurchRole")]
    public class ChurchRole
    {
        public ChurchRole()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.People = new HashSet<Person>();
        }

        public int ChurchRoleId { get; set; }

        [Required]
        [StringLength(50)]
        public string ChurchRoleDescription { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}
