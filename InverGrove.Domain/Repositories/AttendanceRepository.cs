using InverGrove.Data;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Utils;
using InverGrove.Domain.Extensions;
using System.Data.SqlClient;
using System;

namespace InverGrove.Domain.Repositories
{
    public class AttendanceRepository : EntityRepository<Data.Entities.Attendance, int>, IAttendanceRepository
    {
        private readonly object syncRoot = new object();

        public AttendanceRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Adds the specified attendance.
        /// </summary>
        /// <param name="attendance">The attendance.</param>
        /// <returns></returns>
        /// <exception cref="System.ApplicationException">Error occurred in attempting to add Attendance with PersonId:  +
        ///                         attendance.PersonId +  with message:  + sql.Message</exception>
        public int Add(IAttendance attendance)
        {
            Guard.ParameterNotNull(attendance, "attendance");

            attendance.DateAttended = DateTime.Now;

            var newEntityAttendance = ((Attendance)attendance).ToEntity();
            newEntityAttendance.Person = null;
            newEntityAttendance.AbsentReason = null;


            this.Insert(newEntityAttendance);

            using (TimedLock.Lock(this.syncRoot))
            {
                try
                {
                    this.Save();
                }
                catch (SqlException sql)
                {
                    throw new ApplicationException("Error occurred in attempting to add Attendance with PersonId: " +
                        attendance.PersonId + " with message: " + sql.Message);
                }
            }


            return newEntityAttendance.AttendanceId;
        }
    }
}
