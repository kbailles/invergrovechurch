using System;
using System.Collections.Generic;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            this.attendanceRepository = attendanceRepository;
        }

        //public IEnumerable<Domain.ViewModels.AttendancePerson> ShowEveryonesAttendance()
        //{
        //    // TODO - Takes real dates, refactor tests for this method
        //    var fiveWeeksAgo = DateTime.Now.AddDays(-36);

        //    var startTime = fiveWeeksAgo;
        //    var endTime = DateTime.Today;  // end time might not be now() - they may be trying to look at some past increment

        //    var peoplesAttendance = this.attendanceRepository.GetAttendance(startTime, endTime);
        //    return peoplesAttendance;
        //}
    }
}
