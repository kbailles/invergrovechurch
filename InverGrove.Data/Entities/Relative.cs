using System;
using System.ComponentModel.DataAnnotations;

namespace InverGrove.Data.Entities
{
    public class Relative
    {
        [Key]
        public int RelativesId { get; set; }

        public int PersonA { get; set; }

        public int PersonB { get; set; }

        public int RelationTypeId { get; set; }

        public DateTime LastUpdated { get; set; }

        public virtual Person Person { get; set; }

        public virtual Person Person1 { get; set; }

        public virtual RelationType RelationType { get; set; }
    }
}