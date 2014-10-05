namespace InverGrove.Domain.Interfaces
{
    public interface IMembershipRepository : IEntityRepository<Data.Entities.Membership, int>
    {
        /// <summary>
        /// Adds the specified membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">profile</exception>
        int Add(IMembership membership);

        /// <summary>
        /// Updates the specified membership.
        /// </summary>
        /// <param name="membership">The membership.</param>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">membership</exception>
        void Update(IMembership membership);
    }
}