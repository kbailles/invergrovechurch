using System;
using System.Collections.Generic;
using InverGrove.Domain.Resources;

namespace InverGrove.Domain.Events
{
    public class LogServiceEventArgs : EventArgs
    {
        private readonly List<LoggedException> loggedExceptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogServiceEventArgs"/> class.
        /// </summary>
        public LogServiceEventArgs()
        {
            this.loggedExceptions = new List<LoggedException>();
        }

        /// <summary>
        /// Gets the <see cref="InverGrove.Domain.Events.LogServiceEventArgs.LoggedException"/> at the specified index.
        /// </summary>
        /// <value></value>
        public LoggedException this[int index]
        {
            get
            {
                try
                {
                    return this.loggedExceptions[index];
                }
                catch (ArgumentOutOfRangeException)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Adds the specified exception.
        /// </summary>
        /// <param name="exception">The exception.</param>
        public void Add(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }

            this.loggedExceptions.Add(new LoggedException(exception));
        }

        /// <summary>
        /// Adds the specified logged exception.
        /// </summary>
        /// <param name="loggedException">The logged exception.</param>
        public void Add(LoggedException loggedException)
        {
            if (loggedException == null)
            {
                throw new ArgumentNullException("loggedException");
            }
            this.loggedExceptions.Add(loggedException);
        }

        /// <summary>
        /// Adds the range.
        /// </summary>
        /// <param name="exceptions">The exceptions.</param>
        public void AddRange(IEnumerable<Exception> exceptions)
        {
            if (exceptions == null)
            {
                this.Add(new ArgumentNullException(Messages.AddRangeFailed));
            }
            else
            {
                foreach (var x in exceptions)
                {
                    this.Add(x);
                }
            }
        }

        /// <summary>
        /// Gets the logged exceptions.
        /// </summary>
        /// <value>The logged exceptions.</value>
        public LoggedException[] LoggedExceptions
        {
            get { return this.loggedExceptions.ToArray(); }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }

        public class LoggedException
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="LoggedException"/> class.
            /// </summary>
            private LoggedException() { }

            /// <summary>
            /// Initializes a new instance of the <see cref="LoggedException"/> class.
            /// </summary>
            /// <param name="e">The e.</param>
            public LoggedException(Exception e)
                : this()
            {
                this.ExceptionTime = DateTime.Now;
                this.Message = e.Message;
                this.ServiceException = e;
            }

            /// <summary>
            /// Gets or sets the message.
            /// </summary>
            /// <value>The message.</value>
            public string Message { get; private set; }

            /// <summary>
            /// Gets or sets the service exception.
            /// </summary>
            /// <value>The service exception.</value>
            public Exception ServiceException { get; private set; }

            /// <summary>
            /// Gets or sets the exception time.
            /// </summary>
            /// <value>The exception time.</value>
            public DateTime ExceptionTime { get; private set; }
        }
    }
}