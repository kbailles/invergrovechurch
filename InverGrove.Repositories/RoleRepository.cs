using System;
using System.Collections.Generic;
using InverGrove.Data;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Models;
using InverGrove.Repositories.Extensions;
using InverGrove.Repositories.Queries;
using Invergrove.Domain.Models;

namespace InverGrove.Repositories
{
    public class RoleRepository : Repository<Role>
    {

        /// <summary>
        /// Gets the role.
        /// </summary>
        /// <param name="id">The role id.</param>
        /// <param name="name">The role name.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">roleDescription</exception>
        public override Role Get(int? id = null, string name = null)
        {
            if ((id != null) && (id <= 0))
            {
                throw new ArgumentException("id");
            }

            if ((id == null) && (string.IsNullOrEmpty(name)))
            {
                throw new ArgumentNullException("name");
            }

            Role role;

            using (var db = (InverGroveContext)contextFactory.GetObjectContext())
            {
                if ((id != null) && (id > 0))
                {
                    role = RoleQueries.GetRoleById(db, (int)id);
                }
                else
                {
                    role = RoleQueries.GetRole(db, name);
                }
            }

            return role;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <param name="filter">The filter (either userId or userName).</param>
        /// <returns></returns>
        public override IEnumerable<Role> GetAll<T1>(T1 filter)
        {
            var dbContext = (InverGroveContext) contextFactory.GetObjectContext();
            var allRoles = dbContext.Set<Data.Entities.Role>();

            //using (var db = (InverGroveContext)contextFactory.GetObjectContext())
            //{
            //    if (filter is int)
            //    {
            //        int userId = int.Parse(filter.ToString());
            //        roles = this.GetRolesByUserId(db, userId).ToSafeList();
            //    }
            //    else if ((filter is string) && (!string.IsNullOrEmpty(filter as string)))
            //    {
            //        roles = this.GetRolesByUserName(db, filter as string).ToSafeList();
            //    }
            //    else
            //    {
            //        roles = RoleQueries.GetRoles(db).ToSafeList();
            //    }
            //}

            return allRoles.ToModelCollection();
        }

        private IEnumerable<Role> GetRolesByUserName(InverGroveContext db, string userName)
        {
            var roleCollection = new List<Role>();

            try
            {
                var roles = RoleQueries.GetRolesByUserName(db, userName);

                if (roles != null)
                {
                    roleCollection.AddRange(roles);
                }
            }
            catch (Exception e)
            {
                //this.logService.WriteToErrorLog(string.Format("RoleDtoRestService.GetRoleCollectionByUserName failed: {0}", e.Message));
                //this.logService.WriteToErrorLog(e);
            }

            return roleCollection;
        }

        private IEnumerable<Role> GetRolesByUserId(InverGroveContext db, int userId)
        {
            var roleCollection = new List<Role>();

            try
            {
                var roles = RoleQueries.GetRolesByUserId(db, userId);

                if (roles != null)
                {
                    roleCollection.AddRange(roles);
                }
            }
            catch (Exception e)
            {
                //this.logService.WriteToErrorLog(string.Format("RoleDtoRestService.GetRoleCollectionByUserName failed: {0}", e.Message));
                //this.logService.WriteToErrorLog(e);
            }

            return roleCollection;
        }

    }
}