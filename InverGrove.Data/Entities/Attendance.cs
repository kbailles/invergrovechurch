using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InverGrove.Data.Entities
{
    [Table("Attendance")]
    public class Attendance
    {
        public int AttendanceId { get; set; }

        public int PersonId { get; set; }

        public DateTime DateAttended { get; set; }

        public bool IsWednesday { get; set; }

        public bool IsSunday { get; set; }

        public bool IsEvening { get; set; }

        public virtual Person Person { get; set; }
    }
}