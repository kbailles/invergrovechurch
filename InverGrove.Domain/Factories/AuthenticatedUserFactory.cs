using System;
using System.Collections.Generic;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Utils;
using InverGrove.Domain.ViewModels;

namespace InverGrove.Domain.Factories
{
    public class AuthenticatedUserFactory
    {
        private static readonly Lazy<AuthenticatedUserFactory> instance = new Lazy<AuthenticatedUserFactory>(() => new AuthenticatedUserFactory());

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static AuthenticatedUserFactory Instance
        {
            get { return instance.Value; }
        }

        /// <summary>
        /// Creates the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="isAuthenticated">if set to <c>true</c> [is authenticated].</param>
        /// <param name="roles">The roles.</param>
        /// <returns></returns>
        public IAuthenticatedUser Create(string userName, bool isAuthenticated, string[] roles)
        {
            if (isAuthenticated)
            {
                Guard.ParameterNotNullOrEmpty(userName, "userName");
                Guard.ParameterNotNull(roles, "roles");
            }

            var authenticatedUser = new AuthenticatedUser
                                    {
                                        UserName = userName,
                                        IsAuthenticated = isAuthenticated
                                    };

            var roleList = new List<string>();
            foreach (var role in roles)
            {
                roleList.Add(role);
            }

            authenticatedUser.Roles = roleList;

            return authenticatedUser;
        }
    }
}