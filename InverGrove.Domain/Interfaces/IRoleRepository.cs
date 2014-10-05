using System.Collections.Generic;

namespace InverGrove.Domain.Interfaces
{
    public interface IRoleRepository : IEntityRepository<Data.Entities.Role, int>
    {
        /// <summary>
        /// Adds the specified role name.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">roleName</exception>
        int Add(string roleName);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        IEnumerable<IRole> GetAll();

        /// <summary>
        /// Updates the specified updated role.
        /// </summary>
        /// <param name="updatedRole">The updated role.</param>
        void UpdateRole(IRole updatedRole);
    }
}