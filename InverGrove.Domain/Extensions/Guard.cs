using System;
using System.Globalization;

namespace InverGrove.Domain.Extensions
{

    public static class Guard
    {

        /// <summary>
        /// Method to protect against null argument values by throwing an <see cref="ArgumentNullException"/>
        /// Call as : Guard.ArgumentNotNull(WhateverObject "WhateverObjec");
        /// </summary>
        /// <param name="argumentValue">the argument value</param>
        /// <param name="argumentName">the argument name</param>
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Method to protect against null or empty string argument values by throwing an <see cref="ArgumentNullException"/> 
        /// for a null parameter value or an <see cref="ArgumentException"/> for a value of empty string.
        /// </summary>
        /// <param name="argumentValue">the argument value</param>
        /// <param name="argumentName">parameter name</param>
        public static void ArgumentNotNullOrEmpty(string argumentValue, string argumentName)
        {
            ArgumentNotNull(argumentValue, "argumentValue");

            if (argumentValue.Length == 0)
            {
                throw new ArgumentException("The argument value cannot be null or empty string.", argumentName);
            }
        }

        /// <summary>
        /// Dates the time not unset.
        /// </summary>
        /// <param name="argumentValue">The argument value.</param>
        /// <param name="argumentName">Name of the argument.</param>
        public static void CheckDateTime(DateTime argumentValue, string argumentName)
        {
            var defaultDateTime = new DateTime();

            if (argumentValue.ToString(CultureInfo.InvariantCulture) == defaultDateTime.ToString(CultureInfo.InvariantCulture)) // better way of expressing this?
            {
                throw new ArgumentException(argumentName);
            }
        }

    }

}