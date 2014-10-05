﻿using System.Collections.Generic;

namespace InverGrove.Domain.Interfaces
{
    public interface IUserRoleRepository : IEntityRepository<Data.Entities.UserRole, int>
    {
        /// <summary>
        /// Adds the users to roles.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="roles">The roles.</param>
        void AddUsersToRoles(IList<string> users, IList<string> roles);

        /// <summary>
        /// Removes the users from roles.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="roles">The roles.</param>
        void RemoveUsersFromRoles(IList<string> users, IList<string> roles);
    }
}