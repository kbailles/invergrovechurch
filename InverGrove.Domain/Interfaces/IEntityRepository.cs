﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using InverGrove.Data;

namespace InverGrove.Domain.Interfaces
{
    public interface IEntityRepository<TEntity, TK>
        // ReSharper restore TypeParameterCanBeVariant
        where TEntity : class
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        IInverGroveContext Context { get; }

        /// <summary>
        /// Gets or sets the default include properties. This needs to be a comma-delimited list of 
        /// properties to include in a query by default.
        /// </summary>
        /// <value>
        /// The default include properties.
        /// </value>
        string DefaultIncludeProperties { get; set; }    

        /// <summary>
        /// Generic method to get a collection of Entities
        /// </summary>
        /// <param name="filter">Filter expression for the return Entities</param>
        /// <param name="orderBy">Represents the order of the return Entities</param>
        /// <param name="doNotTrack">if set to <c>true</c> [do not track].</param>
        /// <param name="includeProperties">Include Properties for the navigation properties</param>
        /// <returns>
        /// A Enumerable of Entities
        /// </returns>
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool doNotTrack = false, string includeProperties = "");

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="doNotTrack">if set to <c>true</c> [do not track].</param>
        /// <param name="includeProperties">The include properties.</param>
        /// <param name="configureAwait">if set to <c>true</c> [configure await].</param>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool doNotTrack = false, string includeProperties = "", bool configureAwait = true);

        /// <summary>
        /// Gets the by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="configureAwait">if set to <c>true</c> [configure await].</param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(TK id, bool configureAwait = true);

        /// <summary>
        /// Generic Method to get an Entity by Identity
        /// </summary>
        /// <param name="id">The Identity of the Entity</param>
        /// <returns>
        /// The Entity
        /// </returns>
        TEntity GetById(TK id);

        /// <summary>
        /// Generic method for add an Entity to the context
        /// </summary>
        /// <param name="entity">The Entity to Add</param>
        void Insert(TEntity entity);

        /// <summary>
        /// Inserts the TEntity asynchronously.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <param name="configureAwait">if set to <c>true</c> [configure await].</param>
        /// <returns></returns>
        Task InsertAsync(TEntity entity, bool configureAwait = true);

        /// <summary>
        /// Generic method for deleting a method in the context by identity
        /// </summary>
        /// <param name="id">The Identity of the Entity</param>
        void Delete(TK id);

        /// <summary>
        /// Generic method for deleting a method in the context pasing the Entity
        /// </summary>
        /// <param name="entityToDelete">Entity to Delete</param>
        void Delete(TEntity entityToDelete);

        /// <summary>
        /// Deletes the asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="configureAwait">if set to <c>true</c> [configure await].</param>
        /// <returns></returns>
        Task DeleteAsync(TK id, bool configureAwait = true);

        /// <summary>
        /// Generic method for updating an Entity in the context
        /// </summary>
        /// <param name="entityToUpdate">The entity to Update</param>
        void Update(TEntity entityToUpdate);

        /// <summary>
        /// Updates the TEntity asynchronously.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <param name="configureAwait">if set to <c>true</c> [configure await].</param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entityToUpdate, bool configureAwait = true);

        /// <summary>
        /// Generic implementation for get Paged Entities
        /// </summary>
        /// <typeparam name="TKey">Key for order Expression</typeparam>
        /// <param name="pageIndex">Index of the Page</param>
        /// <param name="pageCount">Number of Entities to get</param>
        /// <param name="orderByExpression">Order expression</param>
        /// <param name="ascending">If the order is ascending or descending</param>
        /// <returns>
        /// Enumerable of Entities matching the conditions
        /// </returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        IEnumerable<TEntity> GetPagedElements<TKey>(int pageIndex, int pageCount,
            Expression<Func<TEntity, TKey>> orderByExpression, bool ascending = true);

        /// <summary>
        /// Generic implementation for get Paged Entities
        /// </summary>
        /// <typeparam name="TKey">Key for order Expression</typeparam>
        /// <param name="pageIndex">Index of the Page</param>
        /// <param name="pageCount">Number of Entities to get</param>
        /// <param name="orderByExpression">Order expression</param>
        /// <param name="ascending">If the order is ascending or descending</param>
        /// <param name="includeProperties">Includes</param>
        /// <returns>
        /// Enumerable of Entities matching the conditions
        /// </returns>
        /// <exception cref="System.ArgumentNullException"></exception>
        // ReSharper disable MethodOverloadWithOptionalParameter
        IEnumerable<TEntity> GetPagedElements<TKey>(int pageIndex, int pageCount,
            Expression<Func<TEntity, TKey>> orderByExpression, bool ascending = true, string includeProperties = "");

        // ReSharper restore MethodOverloadWithOptionalParameter

        /// <summary>
        /// Execute query
        /// </summary>
        /// <param name="sqlQuery">The Query to be executed</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>
        /// List of Entity
        /// </returns>
        IEnumerable<TEntity> GetFromDatabaseWithQuery(string sqlQuery, params object[] parameters);

        /// <summary>
        /// Execute a command in database
        /// </summary>
        /// <param name="sqlCommand">The sql query</param>
        /// <param name="parameters">The parameters</param>
        /// <returns>integer representing the sql code</returns>
        int ExecuteInDatabaseByQuery(string sqlCommand, params object[] parameters);

        /// <summary>
        /// Get count of Entities
        /// </summary>
        /// <returns></returns>
        int GetCount();

        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();
    }
}