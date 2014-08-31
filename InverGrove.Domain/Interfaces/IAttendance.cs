using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InverGrove.Domain.Interfaces
{
    /// <summary>
    /// Expected to work in conjuction 
    /// </summary>
    public interface IAttendance
    {
        int AttendanceId { get; }
        int PersonId { get; }
        string FirstName { get; set; }
        string LastName { get; set; }
        DateTime DateAttended { get; set; }
        bool IsWednesday { get; set; }
        bool IsSunday { get; set; }
        bool IsEvening { get; set; }  // Sunday PM worship
    }
}
