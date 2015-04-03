using System;
using System.Globalization;
using InverGrove.Domain.Exceptions;

namespace InverGrove.Domain.Utils
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
        /// Method to protect against null argument values by throwing an <see cref="ParameterNullException"/>
        /// </summary>
        /// <param name="argumentValue">The argument value.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">2</exception>
        public static void ParameterNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ParameterNullException(argumentName, 2);
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
            if (string.IsNullOrEmpty(argumentValue))
            {
                throw new ArgumentException("The argument value cannot be null or empty string.", argumentName);
            }
        }

        /// <summary>
        /// Method to protect against null or empty string argument values by throwing an <see cref="ParameterNullException"/>
        /// </summary>
        /// <param name="argumentValue">The argument value.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterNullException">2</exception>
        public static void ParameterNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentValue))
            {
                throw new ParameterNullException(argumentName, 2);
            }
        }

        /// <summary>
        /// Method to protect against argument less than or equal to zero values by throwing an <see cref="ParameterOutOfRangeException"/>
        /// </summary>
        /// <param name="argumentValue">The argument value.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterOutOfRangeException">2</exception>
        public static void ParameterNotOutOfRange(int argumentValue, string argumentName)
        {
            if (argumentValue <= 0)
            {
                throw new ParameterOutOfRangeException(argumentName, 2);
            }
        }

        public static void ParameterNotGreaterThanZero(int argumentValue, string argumentName)
        {
            if (argumentValue < 1)
            {
                throw new ParameterOutOfRangeException(argumentName, 2);
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

        /// <summary>
        /// Parameters the unique identifier not empty.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <param name="argumentName">Name of the argument.</param>
        /// <exception cref="InverGrove.Domain.Exceptions.ParameterGuidException">2</exception>
        public static void ParameterGuidNotEmpty(Guid guid, string argumentName)
        {
            if (guid == Guid.Empty)
            {
                throw new ParameterGuidException(argumentName, 2);
            }
        }
    }
}