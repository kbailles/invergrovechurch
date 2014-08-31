using System;
using System.ComponentModel.DataAnnotations;

namespace InverGrove.Data.Entities
{
    public class MemberNote
    {
        [Key]
        public int PersonNotesId { get; set; }

        public int UserId { get; set; }

        [StringLength(500)]
        public string Note { get; set; }

        public DateTime? DateOfNote { get; set; }

        public virtual User User { get; set; }
    }
}