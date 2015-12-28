using System;

namespace InverGrove.Domain.Interfaces
{
    public interface IManageAttendance
    {
        /// <summary>
        /// Gets or sets the date attended.
        /// </summary>
        /// <value>
        /// The date attended.
        /// </value>
        DateTime DateAttended { get; set; }

        /// <summary>
        /// Gets or sets the attended count.
        /// </summary>
        /// <value>
        /// The attended count.
        /// </value>
        int AttendedCount { get; set; }

        /// <summary>
        /// Gets or sets the absent count.
        /// </summary>
        /// <value>
        /// The absent count.
        /// </value>
        int AbsentCount { get; set; }

        /// <summary>
        /// Gets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        int TotalCount { get; }
    }
}