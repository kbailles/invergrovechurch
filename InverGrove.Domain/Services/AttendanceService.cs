using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;
using InverGrove.Domain.ViewModels;

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

                attendanceList.Add(personAttendance);
            }

            return attendanceList;
        }

        /// <summary>
        /// Gets the past six months base total attendance for past 6 months for the manage attendance page.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IManageAttendance> GetManageAttendanceList()
        {
            var manageAttendanceList = new List<IManageAttendance>();

            // Get all past 6 months of attendance
            var attendanceList =
                this.attendanceRepository.Get(x => (x.DateAttended.Date >= DateTime.Now.Date.AddMonths(-6)))
                    .ToModelCollection()
                    .ToList();

            // Get absent total count per date
            var absentCollection = (from at in attendanceList
                where at.IsAbsent
                group at by at.DateAttended
                into ga
                select new ManageAttendance
                       {
                           DateAttended = ga.Key,
                           AbsentCount = ga.Count()
                       }).ToList();

            // Get attended total count per date
            var attendedCollection = (from at in attendanceList
                where !at.IsAbsent
                group at by at.DateAttended
                into ga
                select new ManageAttendance
                       {
                           DateAttended = ga.Key,
                           AttendedCount = ga.Count()
                       }).ToList();

            // Combine the counts together
            foreach (var absentNumber in absentCollection)
            {
                var manageData = new ManageAttendance
                {
                    DateAttended = absentNumber.DateAttended,
                    AbsentCount = absentNumber.AbsentCount

                };

                var dateAttended = attendedCollection.FirstOrDefault(a => a.DateAttended == absentNumber.DateAttended);

                if (dateAttended != null)
                {
                    manageData.AttendedCount = dateAttended.AttendedCount;
                }

                manageAttendanceList.Add(manageData);

                attendedCollection.Remove(dateAttended);
            }

            if (attendedCollection.Count > 0)
            {
                foreach(var attendedNumber in attendedCollection)
                {
                    var manageData = new ManageAttendance
                    {
                        DateAttended = attendedNumber.DateAttended,
                        AttendedCount = attendedNumber.AbsentCount

                    };

                    manageAttendanceList.Add(manageData);

                }
            }

            return manageAttendanceList.OrderByDescending(x => x.DateAttended);
        }

        /// <summary>
        /// Gets the attendance by date range.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <returns></returns>
        public IEnumerable<IAttendancePerson> GetAttendanceByDateRange(DateTime startDate, DateTime endDate)
        {
            var attendanceList = new List<IAttendancePerson>();

            if(startDate.Date.Equals(endDate.Date))
            {
                endDate = endDate.AddDays(1);
            }

            var peoplesAttendance = this.attendanceRepository.Get(x =>
                (x.DateAttended.Date >= startDate.Date) && (x.DateAttended.Date <= endDate.Date), includeProperties: "AbsentReason,Person");

            return this.GetDetailList(peoplesAttendance);
        }

        /// <summary>
        /// Gets the attendance by date.
        /// </summary>
        /// <param name="attendanceDate">The attendance date.</param>
        /// <returns></returns>
        public IEnumerable<IAttendancePerson> GetAttendanceByDate(DateTime attendanceDate)
        {
            var peoplesAttendance = this.attendanceRepository.Get(x =>
                (x.DateAttended.Date == attendanceDate.Date), includeProperties: "AbsentReason,Person");

            return this.GetDetailList(peoplesAttendance);
        }

        private IEnumerable<IAttendancePerson> GetDetailList(IEnumerable<Data.Entities.Attendance> attendanceCollection)
        {
            var attendanceList = new List<IAttendancePerson>();

            foreach (var attendance in attendanceCollection)
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

                if (attendance.AbsentReason != null)
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
