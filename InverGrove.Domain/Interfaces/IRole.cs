namespace InverGrove.Domain.Interfaces
{
    public interface IRole
    {
        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        int RoleId { get; set; }

        /// <summary>
        /// Gets or sets the descrption.
        /// </summary>
        /// <value>
        /// The descrption.
        /// </value>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        string ErrorMessage { get; set; }
    }
}