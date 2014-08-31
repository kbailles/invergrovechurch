using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InverGrove.Data.Entities
{
    public class MaritalStatus
    {
        public MaritalStatus()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.People = new HashSet<Person>();
        }

        [Key]
        public int MaritalStatusId { get; set; }

        [Required]
        [StringLength(50)]
        public string MaritalStatusDescription { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}