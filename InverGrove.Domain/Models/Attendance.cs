using System;

namespace InverGrove.Domain.Models
{
    public class Attendance : Interfaces.IAttendance
    {
        /// <summary>
        /// Gets or sets the attendance identifier.
        /// </summary>
        /// <value>
        /// The attendance identifier.
        /// </value>
        public int AttendanceId { get; set; }

        /// <summary>
        /// Gets or sets the absent reason identifier.
        /// </summary>
        /// <value>
        /// The absent reason identifier.
        /// </value>
        public int? AbsentReasonId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is absent.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is absent; otherwise, <c>false</c>.
        /// </value>
        public bool IsAbsent { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>
        /// The person identifier.
        /// </value>
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the date attended.
        /// </summary>
        /// <value>
        /// The date attended.
        /// </value>
        public DateTime DateAttended { get; set; }

        /// <summary>
        /// Gets or sets the is wednesday.
        /// </summary>
        /// <value>
        /// The is wednesday.
        /// </value>
        public bool IsWednesday { get; set; }

        /// <summary>
        /// Gets or sets the is sunday.
        /// </summary>
        /// <value>
        /// The is sunday.
        /// </value>
        public bool IsSunday { get; set; }

        /// <summary>
        /// Gets or sets the is Sunday evening.  If Sunday morning then this is false.
        /// </summary>
        /// <value>
        /// The is evening.
        /// </value>
        public bool IsEvening { get; set; }
    }
}
