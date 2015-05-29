using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Models
{
    public class AbsentReason : IAbsentReason
    {
        /// <summary>
        /// Gets or sets the absent reason identifier.
        /// </summary>
        /// <value>
        /// The absent reason identifier.
        /// </value>
        public int AbsentReasonId { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}
