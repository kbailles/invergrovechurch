namespace InverGrove.Domain.Interfaces
{
    public interface IMembershipRepository : IEntityRepository<Data.Entities.Membership, int>
    {
        /// <summary>
        /// Adds the specified membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">profile</exception>
        IMembership Add(IMembership membership, string userName);

        /// <summary>
        /// Updates the specified membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">membership</exception>
        bool Update(IMembership membership);
    }
}