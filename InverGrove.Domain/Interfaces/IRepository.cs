using System.Collections.Generic;

namespace Invergrove.Domain.Interfaces
{
    public interface IRepository<T>
        where T : IResource
    {
        /// <summary>
        /// Adds the specified resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        T Add(T resource);

        /// <summary>
        /// Deletes the specified resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        T Delete(T resource);

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        T Get(int? id = null, string name = null);

        /// <summary>
        /// Gets all.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        IEnumerable<T> GetAll<T1>(T1 filter);

        /// <summary>
        /// Updates the specified resource.
        /// </summary>
        /// <param name="resource">The resource.</param>
        T Update(T resource);
    }
}