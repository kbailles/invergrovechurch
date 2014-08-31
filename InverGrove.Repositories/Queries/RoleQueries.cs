using System;
using System.Collections.Generic;
using System.Linq;
using InverGrove.Data;
using InverGrove.Domain.Models;

namespace InverGrove.Repositories.Queries
{
    internal static class RoleQueries
    {
        internal static readonly Func<InverGroveContext, string, IEnumerable<Role>> GetRolesByUserName =
            (db, userName) => (from u in db.Users
                               join ur in db.UserRoles on u.UserId equals ur.UserId
                               join r in db.Roles on ur.RoleId equals r.RoleId
                               where u.UserName == userName
                               select new Role
                               {
                                   RoleId = r.RoleId,
                                   Description = r.Description
                               });

        internal static readonly Func<InverGroveContext, int, IEnumerable<Role>> GetRolesByUserId =
            (db, userId) => (from u in db.Users
                             join ur in db.UserRoles on u.UserId equals ur.UserId
                             join r in db.Roles on ur.RoleId equals r.RoleId
                             where u.UserId == userId
                             select new Role
                             {
                                 RoleId = r.RoleId,
                                 Description = r.Description
                             });

        internal static readonly Func<InverGroveContext, string, Role> GetRole =
                (db, roleDescription) =>
                    (from r in db.Roles.Where(ri => ri.Description == roleDescription)
                     select new Role
                     {
                         RoleId = r.RoleId,
                         Description = r.Description
                     }).FirstOrDefault();

        internal static readonly Func<InverGroveContext, int, Role> GetRoleById =
                (db, roleId) =>
                    (from r in db.Roles.Where(ri => ri.RoleId == roleId)
                     select new Role
                     {
                         RoleId = r.RoleId,
                         Description = r.Description
                     }).FirstOrDefault();

        internal static readonly Func<InverGroveContext, IEnumerable<Role>> GetRoles =
                (db) =>
                    (from r in db.Roles
                     select new Role
                     {
                         RoleId = r.RoleId,
                         Description = r.Description
                     });
    }
}