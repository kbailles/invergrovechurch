using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InverGrove.Data.Entities
{
    [Table("PhoneNumber")]
    public class PhoneNumber
    {
        public int PhoneNumberId { get; set; }

        [Required]
        [StringLength(10)]
        public string Phone { get; set; }

        [Required]
        public int PersonId { get; set; }

        [Required]
        public int PhoneNumberTypeId { get; set; }

        public virtual Person Person { get; set; }

        public virtual PhoneNumberType PhoneNumberType { get; set; }
    }
}