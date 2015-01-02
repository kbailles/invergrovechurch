using System.Collections.Generic;

namespace InverGrove.Domain.Interfaces
{
    public interface IUserRoleRepository : IEntityRepository<Data.Entities.UserRole, int>
    {
        /// <summary>
        /// Adds the members to roles.
        /// </summary>
        /// <param name="users">The members.</param>
        /// <param name="roles">The roles.</param>
        void AddUsersToRoles(IList<string> users, IList<string> roles);

        /// <summary>
        /// Adds the user to role.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="roleId">The role identifier.</param>
        void AddUserToRole(int userId, int roleId);

        /// <summary>
        /// Removes the members from roles.
        /// </summary>
        /// <param name="users">The members.</param>
        /// <param name="roles">The roles.</param>
        void RemoveUsersFromRoles(IList<string> users, IList<string> roles);
    }
}