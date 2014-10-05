﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using InverGrove.Data;
using InverGrove.Data.Entities;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Repositories
{
    public class UserRoleRepository : EntityRepository<UserRole, int>, IUserRoleRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public UserRoleRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public new static IUserRoleRepository Create()
        {
            return new UserRoleRepository(InverGroveContext.Create());
        }

        /// <summary>
        /// Adds the users to roles.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="roles">The roles.</param>
        public void AddUsersToRoles(IList<string> users, IList<string> roles)
        {
            if ((users == null) || (roles == null))
            {
                return;
            }

            this.PersistUserRoles(users, roles, true);
        }

        /// <summary>
        /// Removes the users from roles.
        /// </summary>
        /// <param name="users">The users.</param>
        /// <param name="roles">The roles.</param>
        public void RemoveUsersFromRoles(IList<string> users, IList<string> roles)
        {
            if ((users == null) || (roles == null))
            {
                return;
            }

            this.PersistUserRoles(users, roles, false);
        }

        private void PersistUserRoles(IList<string> users, IList<string> roles, bool isInsert)
        {
            var foundUsers = this.Get(u => users.Contains(u.User.UserName)).ToSafeList();
            var foundRoles = this.Get(u => roles.Contains(u.Role.Description)).ToSafeList();

            this.SetupUserRoleChanges(isInsert, foundUsers, foundRoles);

            try
            {
                this.Save();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Error when attempting to persist users to roles in UserRoleRepository: " + ex.Message);
            }
        }

        private void SetupUserRoleChanges(bool isInsert, IEnumerable<UserRole> foundUsers, List<UserRole> foundRoles)
        {
            foreach (var user in foundUsers)
            {
                var user1 = user;

                foreach (var role in foundRoles)
                {
                    var role1 = role;
                    var existingUserRole = this.Get(ur => (ur.RoleId == role1.RoleId) && (ur.UserId == user1.UserId)).FirstOrDefault();

                    if (isInsert)
                    {
                        if (existingUserRole == null)
                        {
                            var newUserRole = new UserRole
                            {
                                DateCreated = DateTime.Now,
                                DateModified = DateTime.Now,
                                UserId = user1.UserId,
                                RoleId = role1.RoleId
                            };

                            this.Insert(newUserRole);
                        }
                    }
                    else
                    {
                        this.Delete(existingUserRole);
                    }
                }
            }
        }
    }
}