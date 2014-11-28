using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using InverGrove.Domain.Events;
using InverGrove.Domain.Extensions;
using InverGrove.Domain.Factories;
using InverGrove.Domain.Interfaces;
using InverGrove.Domain.Resources;
using InverGrove.Domain.Utils;
using NLog;

namespace InverGrove.Domain.Services
{
    public class LogService : ILogService
    {
        private const string TwoParameters = "Calling Method:{0}\r\nMessage:{1}";
        private const string CallingParameters = "\r\nCalling Method:{0}\r\nLine number: {1}\r\nMessage:{2}";
        private static readonly object syncRoot = new object();

        // by default we fire off the events, but this lets us turn them off if we like.
        private static bool fireEvents = true;
        private static readonly Logger defaultLog = LogManager.GetLogger("InverGrove.Domain.Services.LogService");

        /// <summary>
        ///   Initializes a new instance of the <see cref="LogService" /> class.
        /// </summary>
        public LogService(string connectionString, bool logToDbase)
        {
            // set the default
            this.SetLogConfiguration(connectionString, logToDbase);

            defaultLog.Info(Messages.DefaultLogCreated);
        }

        /// <summary>
        ///   Gets the internal log.
        /// </summary>
        /// <value> The internal log. </value>
        public Logger StarterKitLog
        {
            get { return defaultLog; }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether [fire events].
        /// </summary>
        /// <value> <c>true</c> if [fire events]; otherwise, <c>false</c> . </value>
        public static bool FireEvents
        {
            get { return fireEvents; }
            set { fireEvents = value; }
        }

        /// <summary>
        ///   Gets or sets a value indicating whether [generate stack references].
        /// </summary>
        /// <value> <c>true</c> if [generate stack references]; otherwise, <c>false</c> . </value>
        public static bool GenerateStackReferences { get; set; }

        /// <summary>
        /// Sets the environment to use environment specific logger.config
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="logToDbase">if set to <c>true</c> [log to dbase].</param>
        /// <returns></returns>
        public bool SetLogConfiguration(string connectionString, bool logToDbase)
        {
            using (TimedLock.Lock(syncRoot))
            {
                 LogConfigurationFactory.Instance.SetLogLevelConfigeration(connectionString, logToDbase);
            }
            return this.ValidateInternalLog();
        }

        /// <summary>
        /// Flushes this instance.
        /// </summary>
        public virtual void Flush() { }

        /// <summary>
        /// Writes to debug log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <param name="callerLineNumber">The caller line number.</param>
        /// <returns>
        ///   <c>true</c> if successful, otherwise <c>false</c>
        /// </returns>
        public bool WriteToDebugLog(string message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
#if DEBUG
            if (GenerateStackReferences)
            {
                var frame = new StackFrame(1).GetMethod().DeclaringType;
                if (!ReferenceEquals(null, frame))
                {
                    defaultLog.Debug(TwoParameters, (object)frame.FullName, (object)message);
                }
                else
                {
                    defaultLog.Debug(string.Format(CallingParameters, callerMemberName, callerLineNumber, message));
                }
            }
            else
            {
                defaultLog.Debug(string.Format(CallingParameters, callerMemberName, callerLineNumber, message));
            }
#endif
            return true;
        }

        /// <summary>
        /// Writes to debug log asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <param name="callerLineNumber">The caller line number.</param>
        /// <returns></returns>
        public async Task WriteToDebugLogAsync(string message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0)
        {
             await Task.Run(() => this.WriteToDebugLog(message, callerMemberName, callerMemberName));
        }

        /// <summary>
        ///   Writes to debug log.
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="variables"> The variables. </param>
        /// <returns> <c>true</c> if successful, otherwise <c>false</c> </returns>
        public bool WriteToDebugLog(string message, params object[] variables)
        {
#if DEBUG
            if (GenerateStackReferences)
            {
                var frame = new StackFrame(1).GetMethod().DeclaringType;
                if (!ReferenceEquals(null, frame))
                {
                    defaultLog.Debug(TwoParameters, (object)frame.FullName, string.Format(CultureInfo.InvariantCulture, message, variables));
                }
                else
                {
                    defaultLog.Debug(string.Format(CultureInfo.InvariantCulture, message, variables));
                }
            }
            else
            {
                defaultLog.Debug(message, variables);
            }
#endif
            return true;
        }

        /// <summary>
        /// Writes to debug log asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="variables">The variables.</param>
        /// <returns></returns>
        public async Task WriteToDebugLogAsync(string message, params object[] variables)
        {
             await Task.Run(() => this.WriteToDebugLog(message, variables));
        }

        /// <summary>
        ///   Writes to debug log.
        /// </summary>
        /// <param name="ex"> The ex. </param>
        /// <returns> <c>true</c> if successful, otherwise <c>false</c> </returns>
        public bool WriteToDebugLog(Exception ex)
        {
#if DEBUG
            if (ex == null)
            {
                if (GenerateStackReferences)
                {
                    var frame = new StackFrame(1).GetMethod().DeclaringType;
                    if (!ReferenceEquals(null, frame))
                    {
                        defaultLog.Debug(TwoParameters, (object)frame.FullName, Messages.NullExceptionObject);
                    }
                    else
                    {
                        defaultLog.Debug(Messages.NullExceptionObject);
                    }
                }
                else
                {
                    defaultLog.Error(Messages.NullExceptionObject);
                }

                return false;
            }

            if (GenerateStackReferences)
            {

                var frame = new StackFrame(1).GetMethod().DeclaringType;
                if (!ReferenceEquals(null, frame))
                {
                    defaultLog.Debug(TwoParameters, (object)frame.FullName, ex.Message);
                }
                else
                {
                    defaultLog.Debug(ex.Message);
                }
            }
            else
            {
                defaultLog.Debug(ex.Message);
            }
#endif
            return true;
        }

        /// <summary>
        /// Writes to debug log asynchronously.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public async Task WriteToDebugLogAsync(Exception ex)
        {
             await Task.Run(() =>  this.WriteToDebugLog(ex));
        }

        /// <summary>
        ///   Writes to log.
        /// </summary>
        /// <param name="message"> The message. </param>
        public void WriteToLog(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = Messages.LogExceptionNull;
            }
            defaultLog.Info(message);

            if (fireEvents)
            {
                this.FireInfo(message);
            }
        }

        /// <summary>
        /// Writes to log asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task WriteToLogAsync(string message)
        {
             await Task.Run(() => this.WriteToLog(message));
        }

        /// <summary>
        ///   Writes to log.
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="variables"> The variables. </param>
        public void WriteToLog(string message, params object[] variables)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = Messages.LogExceptionNull;
            }
            if (variables != null)
            {
                for (int i = 0; i < variables.Length; i++)
                {
                    message += "{" + i + " }";
                }
            }
            else
            {
                variables = new object[]
                {
                    "empty"
                };
                message += "{0 }";
            }
            if (GenerateStackReferences)
            {
                string msg = string.Format(CultureInfo.InvariantCulture, message, variables);
                defaultLog.Info(TwoParameters, new StackFrame(1).GetMethod().Name, msg);
                if (fireEvents)
                {
                    this.FireInfo(msg);
                }
            }
            else
            {
                defaultLog.Info(message, variables);
                if (fireEvents)
                {
                    this.FireInfo(string.Format(CultureInfo.InvariantCulture, message, variables));
                }
            }
        }

        /// <summary>
        /// Writes to log asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="variables">The variables.</param>
        /// <returns></returns>
        public async Task WriteToLogAsync(string message, params object[] variables)
        {
             await Task.Run(() => this.WriteToLog(message, variables));
        }

        /// <summary>
        ///   Writes to warn log.
        /// </summary>
        /// <param name="message"> The message. </param>
        public void WriteToWarnLog(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = Messages.WarnLogMessageEmpty;
            }
            if (GenerateStackReferences)
            {
                string msg = string.Format(TwoParameters, new StackFrame(1).GetMethod().Name, message);
                defaultLog.Warn(msg);
                if (fireEvents)
                {
                    this.FireWarn(msg);
                }
            }
            else
            {
                defaultLog.Warn(message);
                if (fireEvents)
                {
                    this.FireWarn(message);
                }
            }
        }

        /// <summary>
        /// Writes to warn log asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task WriteToWarnLogAsync(string message)
        {
            await Task.Run(() => this.WriteToWarnLog(message));
        }

        /// <summary>
        ///   Writes to warn log.
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="variables"> The variables. </param>
        public void WriteToWarnLog(string message, params object[] variables)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = Messages.WarnLogMessageEmpty;
            }
            if (variables != null)
            {
                for (int i = 0; i < variables.Length; i++)
                {
                    message += "{" + i + " }";
                }
            }
            else
            {
                variables = new object[]
                {
                    "empty"
                };
                message += "{0 }";
            }

            if (GenerateStackReferences)
            {
                string msg = string.Format(CultureInfo.InvariantCulture,
                    TwoParameters, new StackFrame(1).GetMethod().Name,
                    string.Format(message, variables));
                defaultLog.Warn(msg);
                if (fireEvents)
                {
                    this.FireWarn(msg);
                }
            }
            else
            {
                defaultLog.Warn(message, variables);
                if (fireEvents)
                {
                    this.FireWarn(string.Format(CultureInfo.InvariantCulture, message, variables));
                }
            }
        }

        /// <summary>
        /// Writes to warn log asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="variables">The variables.</param>
        /// <returns></returns>
        public async Task WriteToWarnLogAsync(string message, params object[] variables)
        {
             await Task.Run(() => this.WriteToWarnLog(message, variables));
        }

        /// <summary>
        ///   Writes to warn log.
        /// </summary>
        /// <param name="ex"> The ex. </param>
        public void WriteToWarnLog(Exception ex)
        {
            if (ex == null)
            {
                return;
            }
            if (GenerateStackReferences)
            {
                string msg = string.Format(CultureInfo.InvariantCulture,
                    TwoParameters, new StackFrame(1).GetMethod().Name, ex.Message);
                defaultLog.Warn(msg);
                if (fireEvents)
                {
                    this.FireWarn(msg);
                }
            }
            else
            {
                defaultLog.Warn(TwoParameters, ex.Message, ex.StackTrace);
                if (fireEvents)
                {
                    this.FireWarn(ex.Message);
                }
            }
        }

        /// <summary>
        /// Writes to warn log asynchronously.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public async Task WriteToWarnLogAsync(Exception ex)
        { 
            await Task.Run(() => this.WriteToWarnLog(ex));
        }

        /// <summary>
        ///   Writes to error log.
        /// </summary>
        /// <param name="message"> The message. </param>
        public void WriteToErrorLog(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = Messages.ErrorMessageEmpty;
            }
            if (GenerateStackReferences)
            {
                string msg = string.Format(CultureInfo.InvariantCulture,
                    TwoParameters, new StackFrame(1).GetMethod().Name, message);
                defaultLog.Error(msg);
                if (fireEvents)
                {
                    this.FireError(msg);
                }
            }
            else
            {
                defaultLog.Error(message);
                if (fireEvents)
                {
                    this.FireError(message);
                }
            }
        }

        /// <summary>
        /// Writes to error log asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task WriteToErrorLogAsync(string message)
        {
            await Task.Run(() => this.WriteToErrorLog(message));
        }

        /// <summary>
        ///   Writes to error log.
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="variables"> The variables. </param>
        public void WriteToErrorLog(string message, params object[] variables)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = Messages.ErrorMessageEmpty;
                if (variables != null)
                {
                    for (int i = 0; i < variables.Length; i++)
                    {
                        message += "{" + i + " }";
                    }
                }
                else
                {
                    variables = new object[]
                    {
                        "empty"
                    };
                    message += "{0 }";
                }
            }

            if (GenerateStackReferences)
            {
                string msg = string.Format(CultureInfo.InvariantCulture,
                    TwoParameters, new StackFrame(1).GetMethod().Name,
                    string.Format(CultureInfo.InvariantCulture, message, variables));
                defaultLog.Error(msg);
                if (fireEvents)
                {
                    this.FireError(msg);
                }
            }
            else
            {
                defaultLog.Error(message, variables);
                if (fireEvents)
                {
                    this.FireError(string.Format(CultureInfo.InvariantCulture, message, variables));
                }
            }
        }

        /// <summary>
        /// Writes to error log asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="variables">The variables.</param>
        /// <returns></returns>
        public async Task WriteToErrorLogAsync(string message, params object[] variables)
        {
           await Task.Run(() => this.WriteToErrorLog(message, variables));
        }

        /// <summary>
        ///   Writes to error log.
        /// </summary>
        /// <param name="ex"> The ex. </param>
        public void WriteToErrorLog(Exception ex)
        {
            if (ex == null)
            {
                ex = new Exception("Null exception object passed to logger.");
            }

            if (GenerateStackReferences)
            {
                string msg = string.Format(CultureInfo.InvariantCulture,
                    TwoParameters, new StackFrame(1).GetMethod().Name, ex.Message);

                defaultLog.Error(msg);

                if (fireEvents)
                {
                    this.FireError(msg);
                }
            }
            else
            {
                defaultLog.Error(TwoParameters, ex.Message, ex.StackTrace);
                if (fireEvents)
                {
                    this.FireError(ex.Message);
                }
            }
        }

        /// <summary>
        /// Writes to error log asynchronously.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public async Task WriteToErrorLogAsync(Exception ex)
        {
            await Task.Run(() => this.WriteToErrorLog(ex));
        }

        /// <summary>
        ///   Writes to fatal log.
        /// </summary>
        /// <param name="message"> The message. </param>
        public void WriteToFatalLog(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = Messages.FatalLogMessageEmpty;
            }

            if (GenerateStackReferences)
            {
                string msg = string.Format(CultureInfo.InvariantCulture,
                    TwoParameters, new StackFrame(1).GetMethod().Name, message);
                defaultLog.Fatal(msg);
                if (fireEvents)
                {
                    this.FireFatal(msg);
                }
            }
            else
            {
                defaultLog.Fatal(message);
                if (fireEvents)
                {
                    this.FireFatal(message);
                }
            }
        }

        /// <summary>
        /// Writes to fatal log asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public async Task WriteToFatalLogAsync(string message)
        {
            await Task.Run(() => this.WriteToFatalLog(message));
        }

        /// <summary>
        ///   Writes to fatal log.
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="variables"> The variables. </param>
        public void WriteToFatalLog(string message, params object[] variables)
        {
            if (string.IsNullOrEmpty(message))
            {
                message = Messages.FatalLogMessageEmpty;
                if (variables != null)
                {
                    for (int i = 0; i < variables.Length; i++)
                    {
                        message += "{" + i + " }";
                    }
                }
                else
                {
                    variables = new object[]
                    {
                        "empty"
                    };
                    message += "{0 }";
                }
            }
            if (GenerateStackReferences)
            {
                string msg = string.Format(CultureInfo.InvariantCulture,
                    TwoParameters, new StackFrame(1).GetMethod().Name,
                    string.Format(message, variables));
                defaultLog.Fatal(msg);
                if (fireEvents)
                {
                    this.FireFatal(msg);
                }
            }
            else
            {
                defaultLog.Fatal(message, variables);
                if (fireEvents)
                {
                    this.FireFatal(string.Format(message, variables));
                }
            }
        }

        /// <summary>
        ///   Writes to fatal log.
        /// </summary>
        /// <param name="ex"> The ex. </param>
        public void WriteToFatalLog(Exception ex)
        {
            if (ex == null)
            {
                ex = new Exception("Null exception object passed to logger.");
            }
            if (GenerateStackReferences)
            {
                string msg = string.Format(CultureInfo.InvariantCulture,
                    TwoParameters, new StackFrame(1).GetMethod().Name, ex.Message);
                defaultLog.Fatal(msg);
                if (fireEvents)
                {
                    this.FireFatal(msg);
                }
            }
            else
            {
                defaultLog.Fatal(TwoParameters, ex.Message, ex.StackTrace);
                if (fireEvents)
                {
                    this.FireFatal(ex.Message);
                }
            }
        }

        /// <summary>
        /// Writes to fatal log asynchronously.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        public async Task WriteToFatalLogAsync(Exception ex)
        {
            await Task.Run(() => this.WriteToDebugLog(ex));
        }

        /// <summary>
        ///   Occurs when [info log message recorded].
        /// </summary>
        public event EventHandler<LogServiceEventArgs> InfoLogMessageRecorded;

        /// <summary>
        ///   Occurs when [warn log message recorded].
        /// </summary>
        public event EventHandler<LogServiceEventArgs> WarnLogMessageRecorded;

        /// <summary>
        ///   Occurs when [fatal log message recorded].
        /// </summary>
        public event EventHandler<LogServiceEventArgs> FatalLogMessageRecorded;

        /// <summary>
        ///   Occurs when [error log message recorded].
        /// </summary>
        public event EventHandler<LogServiceEventArgs> ErrorLogMessageRecorded;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private bool ValidateInternalLog()
        {
            // require that at the least warnings
            // are ALWAYS enabled.
            return defaultLog.IsWarnEnabled && defaultLog.IsErrorEnabled;
        }

        private void FireInfo(string message)
        {
            this.InfoLogMessageRecorded.Raise(this, new LogServiceEventArgs
            {
                Message = message
            });
        }

        private void FireWarn(string message)
        {
            this.WarnLogMessageRecorded.Raise(this, new LogServiceEventArgs
            {
                Message = message
            });
        }

        private void FireError(string message)
        {
            this.ErrorLogMessageRecorded.Raise(this, new LogServiceEventArgs
            {
                Message = message
            });
        }

        private void FireFatal(string message)
        {
            this.FatalLogMessageRecorded.Raise(this, new LogServiceEventArgs
            {
                Message = message
            });
        }
    }
}