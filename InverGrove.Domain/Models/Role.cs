using InverGrove.Domain.Interfaces;
using Invergrove.Domain.Models;

namespace InverGrove.Domain.Models
{
    public class Role : Resource, IRole
    {
        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the descrption.
        /// </summary>
        /// <value>
        /// The descrption.
        /// </value>
        public string Description { get; set; }
    }
}