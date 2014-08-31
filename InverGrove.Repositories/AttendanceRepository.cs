using System;
using System.Collections.Generic;
using System.Linq;
using InverGrove.Data;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.ViewModels;

namespace InverGrove.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly IDataContextFactory context;

        public AttendanceRepository(IDataContextFactory dataContext)
        {
            this.context = dataContext;
        }

        /// <summary>
        /// Returns viewmodel containing Attendance info with person info tagged onto it.
        /// Parms must be deliverate, no coalesce setting of weekEnding as it would obscure errors
        /// </summary>
        /// <param name="weekStarting">The week starting.</param>
        /// <param name="weekEnding">The week ending.  </param>
        /// <returns></returns>
        public IEnumerable<Attendance> GetAttendance(DateTime weekStarting, DateTime weekEnding)
        {
            Guard.CheckDateTime(weekStarting, "weekStarting");
            Guard.CheckDateTime(weekEnding, "weekEnding");

            List<Attendance> peoplesAttendance;

            using (var db = (InverGroveContext)this.context.GetObjectContext())
            {
                peoplesAttendance = (from a in db.Attendances
                                         join u in db.Users on a.UserId equals u.UserId
                                         join p in db.Profiles on u.UserId equals p.UserId
                                         where a.DateAttended > weekStarting && a.DateAttended < weekEnding
                                         orderby a.DateAttended, a.UserId
                                         select new Attendance
                                         {
                                             AttendanceId = a.AttendanceId,
                                             PersonId = u.UserId,
                                             FirstName = p.Person.FirstName,
                                             LastName = p.Person.LastName,
                                             DateAttended = a.DateAttended,
                                             IsEvening = a.IsEvening != null && (bool)a.IsEvening,
                                             IsSunday = a.IsSunday != null && (bool)a.IsSunday,
                                             IsWednesday = a.IsWednesday != null && (bool)a.IsWednesday
                                         }).ToList();
            }

            return peoplesAttendance;
        }
    }
}
