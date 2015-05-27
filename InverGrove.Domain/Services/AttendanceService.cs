using System;
using System.Collections.Generic;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.ViewModels;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAttendanceRepository attendanceRepository;
        private readonly IPersonRepository personRepository;

        public AttendanceService(IAttendanceRepository attendanceRepository, IPersonRepository personRepository)
        {
            this.attendanceRepository = attendanceRepository;
            this.personRepository = personRepository;
        }

        /// <summary>
        /// Adds the attendance.
        /// </summary>
        /// <param name="memberAttendance">The member attendance.</param>
        /// <returns></returns>
        public bool AddAttendance(IEnumerable<IAttendancePerson> memberAttendance)
        {
            Guard.ParameterNotNull(memberAttendance, "memberAttendance");
            bool success = true;

            foreach(var ma in memberAttendance)
            {
                var attendanceId = this.attendanceRepository.Add(ma);
                ma.AttendanceId = attendanceId;

                if (attendanceId <= 0)
                {
                    success = false;
                }
            }

            return success;
        }

        /// <summary>
        /// Gets the members for attendance.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IAttendancePerson> GetMembersForAttendance()
        {
            var attendanceList = new List<IAttendancePerson>();

            var members = this.personRepository.Get(x => x.IsMember);

            foreach(var member in members)
            {
                var personAttendance = new AttendancePerson
                {
                    DateAttended = DateTime.Now,
                    PersonId = member.PersonId,
                    FirstName = member.FirstName,
                    LastName = member.LastName
                };
            }

            return attendanceList;
        }

        /// <summary>
        /// Gets the attendance by date.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
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

                attendanceList.Add(personAttendance);
            }

            return attendanceList;
        }
    }
}
