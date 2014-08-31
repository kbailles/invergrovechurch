using System;
using System.Collections.Generic;
using InverGrove.Domain.ViewModels;

namespace InverGrove.Domain.Interfaces
{
    public interface IAttendanceRepository
    {
        /// <summary>
        /// Returns viewmodel containing Attendance info with person info tagged onto it.
        /// </summary>
        /// <param name="weekStarting">The week starting.</param>
        /// <param name="weekEnding">The week ending.</param>
        /// <returns></returns>
        IEnumerable<Attendance> GetAttendance(DateTime weekStarting, DateTime weekEnding);
    }
}
