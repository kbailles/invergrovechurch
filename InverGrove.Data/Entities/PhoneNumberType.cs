using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InverGrove.Data.Entities
{
    [Table("PhoneNumberType")]
    public class PhoneNumberType
    {
        public PhoneNumberType()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.PhoneNumbers = new HashSet<PhoneNumber>();
        }
        public int PhoneNumberTypeId { get; set; }

        [Required]
        [StringLength(25)]
        public string Description { get; set; }

        public virtual ICollection<PhoneNumber> PhoneNumbers { get; set; }
    }
}