using System.Collections.Generic;
using System.Collections.Specialized;

namespace InverGrove.Domain.Interfaces
{
    public interface IRoleProvider
    {
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
        bool IsUserInRole(string userName, string roleName);

        /// <summary>
        /// Determines whether [is user in role] [the specified user name].
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="roleId">The role id.</param>
        /// <returns>
        ///   <c>true</c> if [is user in role] [the specified user name]; otherwise, <c>false</c>.
        /// </returns>
        bool IsUserInRole(string userName, int roleId);

        /// <summary>
        /// Gets the roles for user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        string[] GetRolesForUser(string userName);

        /// <summary>
        /// Gets all roles for user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        IEnumerable<IRole> GetAllRolesForUser(string userName);

        /// <summary>
        /// Adds a new role to the data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to create.</param>
        void CreateRole(string roleName);

        /// <summary>
        /// Removes a role from the data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to delete.</param>
        /// <param name="throwOnPopulatedRole">If true, throw an exception if <paramref name="roleName" /> has one or more members and do not delete <paramref name="roleName" />.</param>
        /// <returns>
        /// true if the role was successfully deleted; otherwise, false.
        /// </returns>
        bool DeleteRole(string roleName, bool throwOnPopulatedRole);

        /// <summary>
        /// Gets a value indicating whether the specified role name already exists in the role data source for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to search for in the data source.</param>
        /// <returns>
        /// true if the role name already exists in the data source for the configured applicationName; otherwise, false.
        /// </returns>
        bool RoleExists(string roleName);

        /// <summary>
        /// Adds the members to roles.
        /// </summary>
        /// <param name="userNames">The user names.</param>
        /// <param name="roleNames">The role names.</param>
        void AddUsersToRoles(string[] userNames, string[] roleNames);

        /// <summary>
        /// Removes the members from roles.
        /// </summary>
        /// <param name="userNames">The user names.</param>
        /// <param name="roleNames">The role names.</param>
        void RemoveUsersFromRoles(string[] userNames, string[] roleNames);

        /// <summary>
        /// Gets a list of members in the specified role for the configured applicationName.
        /// </summary>
        /// <param name="roleName">The name of the role to get the list of members for.</param>
        /// <returns>
        /// A string array containing the names of all the members who are members of the specified role for the configured applicationName.
        /// </returns>
        string[] GetUsersInRole(string roleName);

        /// <summary>
        /// Gets a list of all the roles for the configured applicationName.
        /// </summary>
        /// <returns>
        /// A string array containing the names of all the roles stored in the data source for the configured applicationName.
        /// </returns>
        string[] GetAllRoles();

        /// <summary>
        /// Finds the members in role.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <param name="userNameToMatch">The user name to match.</param>
        /// <returns></returns>
        string[] FindUsersInRole(string roleName, string userNameToMatch);

        string ApplicationName { get; set; }
        string Name { get; }
        string Description { get; }
        void Initialize(string name, NameValueCollection config);
    }
}