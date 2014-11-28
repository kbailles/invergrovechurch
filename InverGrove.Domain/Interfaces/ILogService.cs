using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using InverGrove.Domain.Events;
using NLog;

namespace InverGrove.Domain.Interfaces
{
    public interface ILogService
    {
        /// <summary>
        ///   Gets the internal log.
        /// </summary>
        /// <value> The internal log. </value>
        Logger StarterKitLog { get; }

        /// <summary>
        /// Sets the environment to use environment specific logger.config
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <param name="logToDbase">if set to <c>true</c> [log to dbase].</param>
        /// <returns></returns>
        bool SetLogConfiguration(string connectionString, bool logToDbase);

        /// <summary>
        /// Flushes this instance.
        /// </summary>
        void Flush();

        /// <summary>
        /// Writes to debug log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <param name="callerLineNumber">The caller line number.</param>
        /// <returns>
        ///   <c>true</c> if successful, otherwise <c>false</c>
        /// </returns>
        bool WriteToDebugLog(string message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0);

        /// <summary>
        /// Writes to debug log asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="callerMemberName">Name of the caller member.</param>
        /// <param name="callerLineNumber">The caller line number.</param>
        /// <returns></returns>
        Task WriteToDebugLogAsync(string message, [CallerMemberName] string callerMemberName = null,
            [CallerLineNumber] int callerLineNumber = 0);

        /// <summary>
        ///   Writes to debug log.
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="variables"> The variables. </param>
        /// <returns> <c>true</c> if successful, otherwise <c>false</c> </returns>
        bool WriteToDebugLog(string message, params object[] variables);

        /// <summary>
        /// Writes to debug log asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="variables">The variables.</param>
        /// <returns></returns>
        Task WriteToDebugLogAsync(string message, params object[] variables);

        /// <summary>
        ///   Writes to debug log.
        /// </summary>
        /// <param name="ex"> The ex. </param>
        /// <returns> <c>true</c> if successful, otherwise <c>false</c> </returns>
        bool WriteToDebugLog(Exception ex);

        /// <summary>
        /// Writes to debug log asynchronously.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        Task WriteToDebugLogAsync(Exception ex);

        /// <summary>
        ///   Writes to log.
        /// </summary>
        /// <param name="message"> The message. </param>
        void WriteToLog(string message);

        /// <summary>
        /// Writes to log asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task WriteToLogAsync(string message);

        /// <summary>
        ///   Writes to log.
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="variables"> The variables. </param>
        void WriteToLog(string message, params object[] variables);

        /// <summary>
        /// Writes to log asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="variables">The variables.</param>
        /// <returns></returns>
        Task WriteToLogAsync(string message, params object[] variables);

        /// <summary>
        ///   Writes to warn log.
        /// </summary>
        /// <param name="message"> The message. </param>
        void WriteToWarnLog(string message);

        /// <summary>
        /// Writes to warn log asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task WriteToWarnLogAsync(string message);

        /// <summary>
        ///   Writes to warn log.
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="variables"> The variables. </param>
        void WriteToWarnLog(string message, params object[] variables);

        /// <summary>
        /// Writes to warn log asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="variables">The variables.</param>
        /// <returns></returns>
        Task WriteToWarnLogAsync(string message, params object[] variables);

        /// <summary>
        ///   Writes to warn log.
        /// </summary>
        /// <param name="ex"> The ex. </param>
        void WriteToWarnLog(Exception ex);

        /// <summary>
        /// Writes to warn log asynchronously.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        Task WriteToWarnLogAsync(Exception ex);

        /// <summary>
        ///   Writes to error log.
        /// </summary>
        /// <param name="message"> The message. </param>
        void WriteToErrorLog(string message);

        /// <summary>
        /// Writes to error log asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task WriteToErrorLogAsync(string message);

        /// <summary>
        ///   Writes to error log.
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="variables"> The variables. </param>
        void WriteToErrorLog(string message, params object[] variables);

        /// <summary>
        /// Writes to error log asynchronously.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="variables">The variables.</param>
        /// <returns></returns>
        Task WriteToErrorLogAsync(string message, params object[] variables);

        /// <summary>
        ///   Writes to error log.
        /// </summary>
        /// <param name="ex"> The ex. </param>
        void WriteToErrorLog(Exception ex);

        /// <summary>
        /// Writes to error log asynchronously.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        Task WriteToErrorLogAsync(Exception ex);

        /// <summary>
        ///   Writes to fatal log.
        /// </summary>
        /// <param name="message"> The message. </param>
        void WriteToFatalLog(string message);

        /// <summary>
        /// Writes to fatal log asynchronous.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        Task WriteToFatalLogAsync(string message);

        /// <summary>
        ///   Writes to fatal log.
        /// </summary>
        /// <param name="message"> The message. </param>
        /// <param name="variables"> The variables. </param>
        void WriteToFatalLog(string message, params object[] variables);

        /// <summary>
        ///   Writes to fatal log.
        /// </summary>
        /// <param name="ex"> The ex. </param>
        void WriteToFatalLog(Exception ex);

        /// <summary>
        /// Writes to fatal log asynchronously.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        Task WriteToFatalLogAsync(Exception ex);

        /// <summary>
        ///   Occurs when [info log message recorded].
        /// </summary>
        event EventHandler<LogServiceEventArgs> InfoLogMessageRecorded;

        /// <summary>
        ///   Occurs when [warn log message recorded].
        /// </summary>
        event EventHandler<LogServiceEventArgs> WarnLogMessageRecorded;

        /// <summary>
        ///   Occurs when [fatal log message recorded].
        /// </summary>
        event EventHandler<LogServiceEventArgs> FatalLogMessageRecorded;

        /// <summary>
        ///   Occurs when [error log message recorded].
        /// </summary>
        event EventHandler<LogServiceEventArgs> ErrorLogMessageRecorded;
    }
}