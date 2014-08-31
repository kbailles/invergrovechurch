using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Threading;

namespace InverGrove.Domain.Exceptions
{
    /// <summary>
    ///   Thrown when a lock times out.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2240:ImplementISerializableCorrectly"), Serializable]
    public class LockTimeoutException : Exception
    {
        private const string LockTimeOut = "Timeout waiting for lock";
#if DEBUG
        private readonly object lockTarget;
        private StackTrace blockingStackTrace;
        private static readonly Hashtable failedLockTargets = new Hashtable();

        /// <summary>
        ///   Sets the stack trace for the given lock target if an error occurred.
        /// </summary>
        /// <param name="lockTarget"> Lock target. </param>
        public static void ReportStackTraceIfError(object lockTarget)
        {
            lock (failedLockTargets)
            {
                if (!failedLockTargets.ContainsKey(lockTarget))
                {
                    return;
                }

                var waitHandle = failedLockTargets[lockTarget] as ManualResetEvent;
                if (waitHandle != null)
                {
                    waitHandle.Set();
                }
                failedLockTargets[lockTarget] = new StackTrace();
                // Also. if you don't call GetBlockingStackTrace()
                // the lockTarget doesn't get removed from the hash
                // table and so we'll always think there's an error
                // here (though no locktimeout exception is thrown).
            }
        }

        /// <summary>
        ///   Creates a new <see cref="LockTimeoutException" /> instance.
        /// </summary>
        /// <remarks>
        ///   Use this exception.
        /// </remarks>
        /// <param name="lockTarget"> Object we tried to lock. </param>
        public LockTimeoutException(object lockTarget)
            : base(LockTimeOut)
        {
            lock (failedLockTargets)
            {
                // This is safer in case somebody forgot to remove
                // the lock target.
                var waitHandle = new ManualResetEvent(false);
                failedLockTargets[lockTarget] = waitHandle;
            }
            this.lockTarget = lockTarget;
        }

        /// <summary>
        ///   Stack trace of the thread that holds a lock on the object this lock is attempting to acquire when it fails.
        /// </summary>
        /// <param name="timeout"> Number of milliseconds to wait for the blocking stack trace. </param>
        public StackTrace GetBlockingStackTrace(int timeout)
        {
            if (timeout < 0)
            {
                throw new ArgumentOutOfRangeException("timeout", timeout, "");
            }

            ManualResetEvent waitHandle;
            lock (failedLockTargets)
            {
                waitHandle = failedLockTargets[this.lockTarget] as ManualResetEvent;
            }
            if (timeout > 0 && waitHandle != null)
            {
                waitHandle.WaitOne(timeout, false);
            }
            lock (failedLockTargets)
            {
                //Hopefully by now we have a stack trace.
                this.blockingStackTrace = failedLockTargets[this.lockTarget] as StackTrace;
            }

            return this.blockingStackTrace;
        }
#endif

        /// <summary>
        ///   Creates a new <see cref="LockTimeoutException" /> instance.
        /// </summary>
        public LockTimeoutException()
            : base(LockTimeOut)
        {
        }

        /// <summary>
        ///   Constructor.
        /// </summary>
        /// <param name="message"> </param>
        public LockTimeoutException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///   Constructor.
        /// </summary>
        /// <param name="message"> </param>
        /// <param name="innerException"> </param>
        public LockTimeoutException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        ///   Constructor.
        /// </summary>
        /// <param name="info"> </param>
        /// <param name="context"> </param>
        protected LockTimeoutException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        ///   Returns a string representation of the exception.
        /// </summary>
        /// <returns> </returns>
        public override string ToString()
        {
            string toString = base.ToString();
#if DEBUG
            if (this.blockingStackTrace != null)
            {
                toString += "\n-------Blocking Stack Trace--------\n" + this.blockingStackTrace;
            }
#endif
            return toString;
        }
    }
}