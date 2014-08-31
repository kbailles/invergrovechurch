using System;
using System.Threading;
using InverGrove.Domain.Exceptions;

namespace InverGrove.Domain.Utils
{
    ///<summary>
    ///  Class provides a nice way of obtaining a lock that will time out with a cleaner syntax than using the whole Monitor.TryEnter() method.
    ///</summary>
    ///<remarks>
    ///  Adapted from Ian Griffiths article http://www.interact-sw.co.uk/iangblog/2004/03/23/locking and incorporating suggestions by Marek Malowidzki as outlined in this blog post http://www.interact-sw.co.uk/iangblog/2004/05/12/timedlockstacktrace
    ///</remarks>
    ///<example>
    ///  Instead of: <code>lock(obj)
    ///                {
    ///                //Thread safe operation
    ///                }
    ///
    ///                do this:
    ///
    ///                using(TimedLock.Lock(obj))
    ///                {
    ///                //Thread safe operations
    ///                }
    ///
    ///                or this:
    ///
    ///                try
    ///                {
    ///                TimedLock timeLock = TimedLock.Lock(obj);
    ///                //Thread safe operations
    ///                timeLock.Dispose();
    ///                }
    ///                catch(LockTimeoutException e)
    ///                {
    ///                Console.WriteLine("Couldn't get a lock!");
    ///                StackTrace otherStack = e.GetBlockingThreadStackTrace(5000);
    ///                if(otherStack == null)
    ///                {
    ///                Console.WriteLine("Couldn't get other stack!");
    ///                }
    ///                else
    ///                {
    ///                Console.WriteLine("Stack trace of thread that owns lock!");
    ///                }
    ///
    ///                }</code>
    ///</example>
    public struct TimedLock : IDisposable
    {

        /// <summary>
        ///   Attempts to obtain a lock on the specified object for up to 10 seconds.
        /// </summary>
        /// <param name="o"> </param>
        /// <returns> </returns>
        public static TimedLock Lock(object o)
        {
            return Lock(o, TimeSpan.FromSeconds(10));
        }

        /// <summary>
        ///   Attempts to obtain a lock on the specified object for up to the specified timeout.
        /// </summary>
        /// <param name="o"> </param>
        /// <param name="timeout"> </param>
        /// <returns> </returns>
        public static TimedLock Lock(object o, TimeSpan timeout)
        {
            Thread.BeginCriticalRegion();
            var tl = new TimedLock(o);
            if (!Monitor.TryEnter(o, timeout))
            {
                // Failed to acquire lock.
#if DEBUG
                GC.SuppressFinalize(tl.leakDetector);
                throw new LockTimeoutException(o);
#else
                                throw new LockTimeoutException();
#endif
            }
            return tl;
        }

        private TimedLock(object o)
        {
            this.target = o;
#if DEBUG
            this.leakDetector = new Sentinel();
#endif
        }

        private readonly object target;

        /// <summary>
        ///   Disposes of this lock.
        /// </summary>
        public void Dispose()
        {
            // Owning thread is done.
#if DEBUG
            try
            {
                //This shouldn't throw an exception.
                LockTimeoutException.ReportStackTraceIfError(this.target);
            }
            finally
            {
                //But just in case...
                Monitor.Exit(this.target);
            }
#else
                        Monitor.Exit(target);
#endif
#if DEBUG
            // It's a bad error if someone forgets to call Dispose,
            // so in Debug builds, we put a finalizer in to detect
            // the error. If Dispose is called, we suppress the
            // finalizer.
            GC.SuppressFinalize(this.leakDetector);
#endif
            Thread.EndCriticalRegion();
        }

#if DEBUG
        // (In Debug mode, we make it a class so that we can add a finalizer
        // in order to detect when the object is not freed.)
        private sealed class Sentinel
        {
            ~Sentinel()
            {
                // If this finalizer runs, someone somewhere failed to
                // call Dispose, which means we've failed to leave
                // a monitor!
                //System.Diagnostics.Debug.Fail("Undisposed lock");
                throw new UndisposedLockException("Undisposed lock");
            }
        }

        private readonly Sentinel leakDetector;
#endif
    }
}
