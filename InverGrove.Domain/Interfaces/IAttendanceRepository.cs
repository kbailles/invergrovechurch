using System;
using System.Collections.Generic;
using InverGrove.Domain.ViewModels;

namespace InverGrove.Domain.Interfaces
{
    public interface IAttendanceRepository : IEntityRepository<Data.Entities.Attendance, int>
    {
        /// <summary>
        /// Adds the specified attendance.
        /// </summary>
        /// <param name="attendance">The attendance.</param>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to add Attendance with PersonId:  +
        ///                         attendance.PersonId +  with message:  + sql.Message</exception>
        int Add(IAttendance attendance);
    }
}
