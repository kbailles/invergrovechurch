using System;
using System.Collections.Generic;
using System.Linq;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using InverGrove.Domain.Repositories;

namespace InverGrove.Domain.Providers
{
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        private readonly IRoleRepository roleRepository;
        private readonly IUserRoleRepository userRoleRepository;

        public RoleProvider()
            : this(null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleProvider" /> class.
        /// </summary>
        /// <param name="roleRepository">The role repository.</param>
        /// <param name="userRoleRepository">The user role repository.</param>
        public RoleProvider(IRoleRepository roleRepository, IUserRoleRepository userRoleRepository)
        {
            this.roleRepository = roleRepository ?? RoleRepository.Create();
            this.userRoleRepository = userRoleRepository ?? UserRoleRepository.Create();
        }

        /// <summary>
        /// Gets a value indicating whether the specified user is in the specified role for
        /// the configured applicationName.
        /// </summary>
        /// <param name="userName">The user name to search for.</param>
        /// <param name="roleName">The role to search in.</param>
        /// <returns>
        /// true if the specified user is in the specified role for the configured
        /// applicationName; otherwise, false.
        /// </returns>
        public override bool IsUserInRole(string userName, string roleName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            if (string.IsNullOrEmpty(roleName))
            {
                throw new ArgumentNullException("roleName");
            }

            string[] userRoles = this.GetRolesForUser(userName);

            if (userRoles == null)
            {
                return false;
            }

            return userRoles.Contains(roleName);
        }

        /// <summary>
        /// Determines whether [is user in role] [the specified user name].
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="roleId">The role id.</param>
        /// <returns>
        ///   <c>true</c> if [is user in role] [the specified user name]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsUserInRole(string userName, int roleId)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            if (roleId <= 0)
            {
                throw new ArgumentException("roleId is invalid in RoleProvider.IsUserInRole");
            }

            var roles = this.GetAllRolesForUser(userName);

            if (roles == null)
            {
                return false;
            }

            return roles.FirstOrDefault(x => x.RoleId == roleId) != null;
        }

        /// <summary>
        /// Gets the roles for user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            List<string> userRoles = new List<string>();
            var roles = this.GetRolesByUserName(userName);

            if (roles != null)
            {
                userRoles = roles.Select(role => role.Description).ToList();
            }

            return userRoles.ToArray();
        }

        /// <summary>
        /// Gets all roles for user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public IEnumerable<IRole> GetAllRolesForUser(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("userName");
            }

            return this.GetRolesByUserName(userName);
        }

        /// <summary>
        /// Adds a new role to the data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        public override void CreateRole(string roleName)
        {
            this.roleRepository.Add(roleName);
        }

        /// <summary>
        /// Removes a role from the data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to delete.</param>
        /// <param name="throwOnPopulatedRole">If true, throw an exception if <paramref name="roleName" /> has one or more members and do not delete <paramref name="roleName" />.</param>
        /// <returns>
        /// true if the role was successfully deleted; otherwise, false.
        /// </returns>
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return true;
            }

            var role = this.roleRepository.Get(r => r.Description == roleName).FirstOrDefault();

            if (role == null)
            {
                return true; // no role to delete
            }

            try
            {
                this.roleRepository.Delete(role.RoleId);
                this.roleRepository.Save();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Gets a value indicating whether the specified role name already exists in the role data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to search for in the data source.</param>
        /// <returns>
        /// true if the role name already exists in the data source for the configured applicationName; otherwise, false.
        /// </returns>
        public override bool RoleExists(string roleName)
        {
            return this.roleRepository.Get(r => r.Description == roleName).Any();
        }

        /// <summary>
        /// Adds the users to roles.
        /// </summary>
        /// <param name="userNames">The user names.</param>
        /// <param name="roleNames">The role names.</param>
        public override void AddUsersToRoles(string[] userNames, string[] roleNames)
        {
            if ((userNames == null) || (roleNames == null))
            {
                return;
            }

            this.userRoleRepository.AddUsersToRoles(userNames.ToList(), roleNames.ToList());
        }

        /// <summary>
        /// Removes the users from roles.
        /// </summary>
        /// <param name="userNames">The user names.</param>
        /// <param name="roleNames">The role names.</param>
        public override void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
        {
            if ((userNames == null) || (roleNames == null))
            {
                return;
            }

            this.userRoleRepository.RemoveUsersFromRoles(userNames.ToList(), roleNames.ToList());
        }

        /// <summary>
        /// Gets a list of users in the specified role for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to get the list of users for.</param>
        /// <returns>
        /// A string array containing the names of all the users who are members of the specified role for the configured applicationName.
        /// </returns>
        public override string[] GetUsersInRole(string roleName)
        {
            var users = new List<string>();
            var userRoles = this.userRoleRepository.Get(u => u.Role.Description == roleName, r => r.OrderBy(c => c.User.UserName));

            foreach (var user in userRoles)
            {
                users.Add(user.User.UserName);
            }

            return users.ToArray();
        }

        /// <summary>
        /// Gets a list of all the roles for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles stored in the data source for the configured applicationName.
        /// </returns>
        public override string[] GetAllRoles()
        {
            var roles = new List<string>();

            foreach (var role in this.roleRepository.GetAll())
            {
                roles.Add(role.Description);
            }

            return roles.ToArray();
        }

        /// <summary>
        /// Finds the users in role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="userNameToMatch">The user name to match.</param>
        /// <returns></returns>
        public override string[] FindUsersInRole(string roleName, string userNameToMatch)
        {
            var users = new List<string>();
            var userRoles = this.userRoleRepository.Get(u =>
                (u.Role.Description == roleName) && (u.User.UserName.Contains(userNameToMatch)));

            foreach (var user in userRoles)
            {
                users.Add(user.User.UserName);
            }

            return users.ToArray();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        private IEnumerable<Role> GetRolesByUserName(string userName)
        {
            var roles = new List<Role>();

            try
            {
                var result = this.userRoleRepository.Get(u => u.User.UserName == userName).ToSafeList();

                foreach (var ur in result)
                {
                    roles.Add(ur.Role.ToModel());
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving roles for user name: " + userName + " with exception: " + ex.Message);
            }

            return roles.OrderBy(r => r.Description);
        }
    }
}