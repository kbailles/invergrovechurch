using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using InverGrove.Domain.Enums;
using InverGrove.Domain.Resources;

namespace InverGrove.Domain.Extensions
{
    public static class EnumerableExtensions
    {
        internal const string OrderByConst = "OrderBy";
        internal const string OrderByDesc = "OrderByDescending";

        /// <summary>
        /// To the safe list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable">The enumerable.</param>
        /// <returns></returns>
        public static List<T> ToSafeList<T>(this IEnumerable<T> enumerable)
        {
            if (ReferenceEquals(null, enumerable))
            {
                return new List<T>();
            }
            return enumerable.ToList();
        }

        /// <summary>
        ///   Orders a queryable by a property with the specified name in the specified direction
        /// </summary>
        /// <param name="queryable"> The data source to order </param>
        /// <param name="propertyName"> The name of the property to order by </param>
        /// <param name="direction"> The direction </param>
        public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> queryable,
            string propertyName, SortDirection direction)
        {
            return queryable.AsQueryable().OrderBy(propertyName, direction);
        }

        /// <summary>
        ///   Orders a queryable by a property with the specified name in the specified direction
        /// </summary>
        /// <param name="queryable"> The data source to order </param>
        /// <param name="propertyName"> The name of the property to order by </param>
        /// <param name="direction"> The direction </param>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyName,
            SortDirection direction)
        {
            //http://msdn.microsoft.com/en-us/library/bb882637.aspx
            if (string.IsNullOrEmpty(propertyName))
            {
                return queryable;
            }

            var type = typeof(T);

            var property = type.GetProperty(propertyName);

            if (property == null)
            {
                throw new InvalidOperationException
                    (string.Format(Messages.PropertyNotFound, propertyName, type));
            }

            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            string methodToInvoke = direction == SortDirection.Ascending ? OrderByConst : OrderByDesc;

            var orderByCall = Expression.Call(typeof(Queryable),
                methodToInvoke,
                new[] { type, property.PropertyType },
                queryable.Expression,
                Expression.Quote(orderByExp));

            return queryable.Provider.CreateQuery<T>(orderByCall);
        }
    }
}