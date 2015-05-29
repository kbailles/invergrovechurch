using System;

namespace InverGrove.Domain.Interfaces
{
    interface IAbsentReason
    {
        /// <summary>
        /// Gets or sets the absent reason identifier.
        /// </summary>
        /// <value>
        /// The absent reason identifier.
        /// </value>
        int AbsentReasonId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        string Description { get; set; }
    }
}
