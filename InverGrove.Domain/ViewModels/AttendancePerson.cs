using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InverGrove.Domain.Interfaces;
using System.Web.Mvc;

namespace InverGrove.Domain.ViewModels
{
    public class AttendancePerson : IAttendancePerson
    {
        /// <summary>
        /// Gets or sets the attendance identifier.
        /// </summary>
        /// <value>
        /// The attendance identifier.
        /// </value>
        public int AttendanceId { get; set; }  // not sure what to do here ideally readonly but we have to set it when casting to this model from EF

        /// <summary>
        /// Gets or sets the absent reason identifier.
        /// </summary>
        /// <value>
        /// The absent reason identifier.
        /// </value>
        public int? AbsentReasonId { get; set; }

        /// <summary>
        /// Gets or sets the absent reason description.
        /// </summary>
        /// <value>
        /// The absent reason description.
        /// </value>
        public string AbsentReasonDescription { get; set; }

        /// <summary>
        /// Gets or sets the person identifier.
        /// </summary>
        /// <value>
        /// The person identifier.
        /// </value>
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string LastName { get; set; }

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

        /// <summary>
        /// Gets or sets the absent reasons.
        /// </summary>
        /// <value>
        /// The absent reasons.
        /// </value>
        public IEnumerable<SelectListItem> AbsentReasons { get; set; }
    }
}
