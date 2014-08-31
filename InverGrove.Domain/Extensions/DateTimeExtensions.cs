using System;
using System.Data.SqlTypes;

namespace InverGrove.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Determines whether [is SQL safe date] [the specified date time].
        /// </summary>
        /// <param name="dateTime">The date time.</param>
        /// <returns></returns>
         public static DateTime IsSqlSafeDate(this DateTime dateTime)
         {
             DateTime minSafeDate = (DateTime) SqlDateTime.MinValue;
             DateTime maxSafeDate = (DateTime) SqlDateTime.MaxValue;

             if (dateTime < minSafeDate)
             {
                 return minSafeDate;
             }

             if (dateTime > maxSafeDate)
             {
                 return maxSafeDate;
             }

             return dateTime;
         }
    }
}