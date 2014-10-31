using System;
using System.Collections.Generic;
using InverGrove.Data;
using InverGrove.Domain.Exceptions;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Interfaces;

namespace InverGrove.Domain.Repositories
{
    public class RoleRepository : EntityRepository<Data.Entities.Role, int>, IRoleRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleRepository"/> class.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public RoleRepository(IInverGroveContext dataContext)
            : base(dataContext)
        {
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public new static IRoleRepository Create()
        {
            return new RoleRepository(InverGroveContext.Create());
        }

        /// <summary>
        /// Adds the specified role name.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns></returns>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">roleName</exception>
        public int Add(string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                throw new ParameterNullException("roleName");
            }

            var newEntityRole = new Data.Entities.Role
            {
                Description = roleName,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            this.Insert(newEntityRole);

            this.Save();

            return newEntityRole.RoleId;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IRole> GetAll()
        {
            var allRoles = base.Get();

            return allRoles.ToModelCollection();
        }

        /// <summary>
        /// Updates the specified updated role.
        /// </summary>
        /// <param name="updatedRole">The updated role.</param>
        public void UpdateRole(IRole updatedRole)
        {
            if (updatedRole == null)
            {
                return;
            }

            var roleEntity = this.GetById(updatedRole.RoleId);

            if (roleEntity != null)
            {
                roleEntity.Description = updatedRole.Description;
                roleEntity.DateModified = DateTime.Now;
            }

            this.Update(roleEntity);

            this.Save();
        }
    }
}