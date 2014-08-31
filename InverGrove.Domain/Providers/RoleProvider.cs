using System;
using System.Collections.Generic;
using System.Linq;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Models;
using Invergrove.Domain.Interfaces;
using Invergrove.Domain.Models;

namespace InverGrove.Domain.Providers
{
    public class RoleProvider : System.Web.Security.RoleProvider
    {
        private readonly IRepository<Role> roleCollectionRepository;

        public RoleProvider()
            : this(null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RoleProvider" /> class.
        /// </summary>
        /// <param name="roleCollectionRepository">The role collection repositoryy.</param>
        public RoleProvider(IRepository<Role> roleCollectionRepository)
        {
            this.roleCollectionRepository = roleCollectionRepository ?? Repository<Role>.Create();
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

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] userNames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] userNames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string userNameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        private IEnumerable<Role> GetRolesByUserName(string userName)
        {
            var result = this.roleCollectionRepository.GetAll(userName).ToSafeList();

            if ((result == null) || !string.IsNullOrEmpty(result[0].ErrorMessage))
            {
                throw new ApplicationException("Error retrieving roles for user name: " + userName);
            }

            return result;
        }
    }
}