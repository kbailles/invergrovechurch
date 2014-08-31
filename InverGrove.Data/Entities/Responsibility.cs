using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InverGrove.Data.Entities
{
    public class Responsibility
    {
        public Responsibility()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.Users = new HashSet<User>();
        }

        [Key]
        public int ResponsibilitiesId { get; set; }

        public bool? IsAssemblyDuty { get; set; }

        public bool? IsMaleOnly { get; set; }

        [StringLength(100)]
        public string Activity { get; set; }

        [Required]
        [StringLength(100)]
        public string ShortDescription { get; set; }

        [Required]
        [StringLength(250)]
        public string LongDescription { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}