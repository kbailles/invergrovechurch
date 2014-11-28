using InverGrove.Domain.Exceptions;
using NLog;
using NLog.Filters;

namespace InverGrove.Domain.Factories
{
    public sealed class NotLoggerFilter : Filter
    {
        private readonly string loggerName;

        /// <summary>
        ///   Initializes a new instance of the <see cref="NotLoggerFilter" /> class.
        /// </summary>
        /// <param name="loggerName"> Name of the logger. </param>
        public NotLoggerFilter(string loggerName)
        {
            if (string.IsNullOrEmpty(loggerName))
            {
                throw new ParameterNullException("loggerName");
            }
            this.loggerName = loggerName.ToUpperInvariant();
        }

        /// <summary>
        /// Checks whether log event should be logged or not.
        /// </summary>
        /// <param name="logEvent">Log event.</param>
        /// <returns>
        ///   <see cref="F:NLog.Filters.FilterResult.Ignore" /> - if the log event should be ignored <br /> <see cref="F:NLog.Filters.FilterResult.Neutral" /> - if the filter doesn't want to decide <br /> <see cref="F:NLog.Filters.FilterResult.Log" /> - if the log event should be logged <br /> .
        /// </returns>
        /// <exception cref="ParameterNullException">logEvent</exception>
        protected override FilterResult Check(LogEventInfo logEvent)
        {
            if (logEvent == null)
            {
                throw new ParameterNullException("logEvent");
            }
            if (logEvent.LoggerName.ToUpperInvariant() != this.loggerName)
            {
                return FilterResult.Ignore;
            }
            return FilterResult.Log;
        }
    }
}