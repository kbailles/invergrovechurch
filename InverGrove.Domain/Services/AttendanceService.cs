using System;
using System.Collections.Generic;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.ViewModels;

namespace InverGrove.Domain.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository attendanceRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository)
        {
            this.attendanceRepository = attendanceRepository;
        }

        public IEnumerable<IAttendancePerson> GetPeopleAttendanceList()
        {
            var attendanceList = new List<IAttendancePerson>();
        }

        public IEnumerable<IAttendancePerson> GetAttendanceByDate(DateTime startDate, DateTime endDate)
        {
            var attendanceList = new List<IAttendancePerson>();

            if(startDate.Date.Equals(endDate.Date))
            {
                endDate.AddDays(1);
            }

            var peoplesAttendance = this.attendanceRepository.Get(x => 
                (x.DateAttended >= startDate) && (x.DateAttended <= endDate), includeProperties: "Person");

            foreach(var attendance in peoplesAttendance)
            {
                var personAttendance = new AttendancePerson 
                { 
                    AttendanceId = attendance.AttendanceId,
                    DateAttended = attendance.DateAttended,
                    IsEvening = attendance.IsEvening,
                    IsSunday = attendance.IsSunday,
                    IsWednesday = attendance.IsWednesday,
                    PersonId = attendance.PersonId
                };

                if (attendance.Person != null)
                {
                    personAttendance.FirstName = attendance.Person.FirstName;
                    personAttendance.LastName = attendance.Person.LastName;
                }
            }
            return attendanceList;
        }
    }
}
