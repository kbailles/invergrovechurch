using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.ViewModels
{
    public class Attendance : IAttendance
    {
        public int AttendanceId { get; set; }  // not sure what to do here ideally readonly but we have to set it when casting to this model from EF
        public int PersonId { get; set; } 

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateAttended { get; set; }
        public bool IsWednesday { get; set; }
        public bool IsSunday { get; set; }
        public bool IsEvening { get; set; }
    }
}
