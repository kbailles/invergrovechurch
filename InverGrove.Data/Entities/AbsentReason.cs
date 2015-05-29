using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InverGrove.Data.Entities
{
    [Table("AbsentReason")]
    public class AbsentReason
    {
        public AbsentReason()
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            this.AttendanceCollection = new HashSet<Attendance>();
        }

        public int AbsentReasonId { get; set; }

        [Required]
        [StringLength(150)]
        public string Description { get; set; }

        public virtual ICollection<Attendance> AttendanceCollection { get; set; }
    }
}
