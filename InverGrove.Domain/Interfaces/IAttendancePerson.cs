using System.Collections.Generic;
using System.Web.Mvc;

namespace InverGrove.Domain.Interfaces
{
    public interface IAttendancePerson : IAttendance
    {
        /// <summary>
        /// Gets or sets the absent reason description.
        /// </summary>
        /// <value>
        /// The absent reason description.
        /// </value>
        string AbsentReasonDescription { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        string LastName { get; set; }

        /// <summary>
        /// Gets or sets the absent reasons.
        /// </summary>
        /// <value>
        /// The absent reasons.
        /// </value>
        IEnumerable<SelectListItem> AbsentReasons { get; set; }
    }
}
