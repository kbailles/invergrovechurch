using Invergrove.Domain.Interfaces;

namespace Invergrove.Domain.Models
{
    public abstract class Resource : IResource
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; set; }
    }
}