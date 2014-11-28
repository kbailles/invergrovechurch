using System;
using InverGrove.Domain.Exceptions;

namespace InverGrove.Domain.Extensions
{
    public static class EventExtensions
    {
        /// <summary>
        /// Raises the specified handler.
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        /// <exception cref="ParameterNullException">sender</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public static void Raise(this EventHandler handler, object sender, EventArgs args)
        {
            if (sender == null)
            {
                throw new ParameterNullException("sender");
            }
            if (args == null)
            {
                throw new ParameterNullException("args");
            }
            if (handler != null)
            {
                handler(sender, args);
            }
        }

        /// <summary>
        /// Raises the specified handler.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler">The handler.</param>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        /// <exception cref="ParameterNullException">sender</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public static void Raise<T>(this EventHandler<T> handler, object sender, T args)
            where T : EventArgs
        {
            if (sender == null)
            {
                throw new ParameterNullException("sender");
            }

            if (args == null)
            {
                throw new ParameterNullException("args");
            }

            if (handler != null)
            {
                handler(sender, args);
            }
        }

        /// <summary>
        /// Determines whether the specified handler is <see langword="null" /> .
        /// </summary>
        /// <param name="handler">The handler.</param>
        /// <returns>
        ///   <c>true</c> if the specified handler is <see langword="null" /> ; otherwise, <c>false</c> .
        /// </returns>
        public static bool IsNull(this EventHandler handler)
        {
            return handler == null;
        }

        /// <summary>
        /// Determines whether the specified handler is <see langword="null" /> .
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="handler">The handler.</param>
        /// <returns>
        ///   <c>true</c> if the specified handler is <see langword="null" /> ; otherwise, <c>false</c> .
        /// </returns>
        public static bool IsNull<T>(this EventHandler<T> handler)
            where T : EventArgs
        {
            return handler == null;
        }
    }
}