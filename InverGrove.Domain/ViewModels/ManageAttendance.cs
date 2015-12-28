using System;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.ViewModels
{
    public class ManageAttendance : IManageAttendance
    {
        /// <summary>
        /// Gets or sets the date attended.
        /// </summary>
        /// <value>
        /// The date attended.
        /// </value>
        public DateTime DateAttended { get; set; }

        /// <summary>
        /// Gets or sets the attended count.
        /// </summary>
        /// <value>
        /// The attended count.
        /// </value>
        public int AttendedCount { get; set; }

        /// <summary>
        /// Gets or sets the absent count.
        /// </summary>
        /// <value>
        /// The absent count.
        /// </value>
        public int AbsentCount { get; set; }

        /// <summary>
        /// Gets the total count.
        /// </summary>
        /// <value>
        /// The total count.
        /// </value>
        public int TotalCount { get { return this.AbsentCount + this.AttendedCount; } }
    }
}