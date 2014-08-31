using System;
using System.Collections.Generic;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using Invergrove.Domain.Interfaces;

namespace Invergrove.Domain.Models
{
    public class Repository<T> : IRepository<T>
        where T : Resource, new()
    {
        protected readonly IDataContextFactory contextFactory;

        protected Repository(IDataContextFactory contextFactory = null)
        {
            this.contextFactory = contextFactory ?? DataContextFactory.Create();
        }

        public static IRepository<T> Create()
        {
            return new Repository<T>();
        }

        /// <summary>
        /// Adds the specified resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual T Add(T resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }

            return null;
        }

        /// <summary>
        /// Deletes the specified resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual T Delete(T resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }

            return null;
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual T Get(int? id = null, string name = null)
        {
            return null;
        }

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual IEnumerable<T> GetAll<T1>(T1 filter)
        {
            return null;
        }

        /// <summary>
        /// Updates the specified resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public virtual T Update(T resource)
        {
            if (resource == null)
            {
                throw new ArgumentNullException("resource");
            }

            return null;
        }
    }
}