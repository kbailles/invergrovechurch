using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InverGrove.Domain.Interfaces
{
    public interface IAttendanceService
    {
        /// <summary>
        /// Adds the attendance.
        /// </summary>
        /// <param name="memberAttendance">The member attendance.</param>
        /// <returns></returns>
        bool AddAttendance(IEnumerable<IAttendancePerson> memberAttendance);

        /// <summary>
        /// Gets the members for attendance.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IAttendancePerson> GetMembersForAttendance();

        /// <summary>
        /// Gets the attendance by date.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        IEnumerable<IAttendancePerson> GetAttendanceByDate(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets the past six months base total attendance for past 6 months for the manage attendance page.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IManageAttendance> GetManageAttendanceList();
    }
}
