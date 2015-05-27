using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InverGrove.Domain.Interfaces
{
    /// <summary>
    /// Expected to work in conjuction 
    /// </summary>
    public interface IAttendance
    {
        /// <summary>
        /// Gets or sets the attendance identifier.
        /// </summary>
        /// <value>
        /// The attendance identifier.
        /// </value>
        int AttendanceId { get; set; }

        /// <summary>
        /// Gets or sets the date attended.
        /// </summary>
        /// <value>
        /// The date attended.
        /// </value>
        DateTime DateAttended { get; set; }

        /// <summary>
        /// Gets or sets the is Sunday evening.  If Sunday morning then this is false.
        /// </summary>
        /// <value>
        /// The is evening.
        /// </value>
        bool IsEvening { get; set; }

        /// <summary>
        /// Gets or sets the is sunday.
        /// </summary>
        /// <value>
        /// The is sunday.
        /// </value>
        bool IsSunday { get; set; }

        /// <summary>
        /// Gets or sets the is wednesday.
        /// </summary>
        /// <value>
        /// The is wednesday.
        /// </value>
        bool IsWednesday { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>
        /// The person identifier.
        /// </value>
        int PersonId { get; set; }
    }
}
