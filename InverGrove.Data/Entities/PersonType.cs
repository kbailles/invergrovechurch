using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InverGrove.Data.Entities
{
    public class PersonType
    {
        public PersonType()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.People = new HashSet<Person>();
        }

        public int PersonTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string PersonTypeDescription { get; set; }

        public virtual ICollection<Person> People { get; set; }
    }
}