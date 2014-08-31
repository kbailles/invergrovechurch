using System;
using System.Runtime.Serialization;
using InverGrove.Domain.Utils;

namespace InverGrove.Domain.Exceptions
{
    /// <summary>
    ///   This exception indicates that a user of the <see cref="TimedLock" /> struct failed to leave a Monitor. This could be the result of a deadlock or forgetting to use the using statement or a try finally block.
    /// </summary>
    [Serializable]
    public class UndisposedLockException : Exception
    {
        /// <summary>
        ///   Constructor.
        /// </summary>
        /// <param name="message"> </param>
        public UndisposedLockException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///   Special constructor used for deserialization.
        /// </summary>
        /// <param name="info"> </param>
        /// <param name="context"> </param>
        protected UndisposedLockException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}