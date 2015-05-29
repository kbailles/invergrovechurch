using System;
using System.Collections.Generic;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.ViewModels;
using InverGrove.Domain.Utils;
using System.Web.Mvc;

namespace InverGrove.Domain.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IAbsentReasonRepository absentRepository;
        private readonly IAttendanceRepository attendanceRepository;
        private readonly IPersonRepository personRepository;

        public AttendanceService(IAbsentReasonRepository absentRepository, IAttendanceRepository attendanceRepository, 
            IPersonRepository personRepository)
        {
            this.absentRepository = absentRepository;
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
            var absentReasons = this.absentRepository.Get();

            var absentReasonSelectList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Select", Value = "0", Selected = true }
            };

            foreach(var reason in absentReasons)
            {
                absentReasonSelectList.Add(new SelectListItem
                {
                    Text = reason.Description,
                    Value = reason.AbsentReasonId.ToString()
                });
            }

            foreach(var member in members)
            {
                var personAttendance = new AttendancePerson
                {
                    DateAttended = DateTime.Now,
                    PersonId = member.PersonId,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    AbsentReasons = absentReasonSelectList
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
                (x.DateAttended >= startDate) && (x.DateAttended <= endDate), includeProperties: "AbsentReason,Person");

            foreach(var attendance in peoplesAttendance)
            {
                var personAttendance = new AttendancePerson 
                { 
                    AttendanceId = attendance.AttendanceId,
                    AbsentReasonId = attendance.AbsentReasonId,
                    DateAttended = attendance.DateAttended,
                    IsEvening = attendance.IsEvening,
                    IsSunday = attendance.IsSunday,
                    IsWednesday = attendance.IsWednesday,
                    PersonId = attendance.PersonId
                };

                if(attendance.AbsentReason != null)
                {
                    personAttendance.AbsentReasonDescription = attendance.AbsentReason.Description;
                }

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
