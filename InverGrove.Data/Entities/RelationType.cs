using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InverGrove.Data.Entities
{
    [Table("RelationType")]
    public class RelationType
    {
        public RelationType()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.Relatives = new HashSet<Relative>();
        }

        public int RelationTypeId { get; set; }

        [Required]
        [StringLength(50)]
        public string RelationTypeDescription { get; set; }

        public virtual ICollection<Relative> Relatives { get; set; }
    }
}