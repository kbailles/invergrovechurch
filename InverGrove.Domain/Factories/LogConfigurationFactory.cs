using System;
using System.Collections.Generic;
using InverGrove.Domain.Utils;
using NLog.Common;
using NLog.Config;
using NLog.Targets;
using NLog;

namespace InverGrove.Domain.Factories
{
    /// <summary>
    ///  This class is used to log information from the starter kit, this ensures that some messages 
    /// will always be logged regardless of the current client configuration.
    /// </summary>
    /// <remarks>
    /// In order to configure NLog in code you must complete the following steps: Create a
    /// LoggingConfiguration object that will hold the configuration Create one or more targets
    /// (objects of classes inheriting from Target) Set the properties of the targets Define 
    /// logging rules through LoggingRule objects and add them to the configuration's LoggingRules
    /// Activate the configuration by assigning the configuration object to LogManager.Configuration
    /// </remarks>
    internal sealed class LogConfigurationFactory
    {
        private static readonly object syncRoot = new object();

        private static readonly LogConfigurationFactory instance = new LogConfigurationFactory();
        private readonly LoggingConfiguration loggingConfiguration;
        private readonly List<LoggingRule> rules = new List<LoggingRule>();
        private readonly Dictionary<string, Target> targets = new Dictionary<string, Target>();

        internal const string LogServiceKey = "InverGrove.Domain.Services.LogService";
        internal const string WebEventKey = "InverGrove.Domain.Services.LoggingWebEventProvider";

        /// <summary>
        ///   Prevents a default instance of the <see cref="LogConfigurationFactory" /> class from being created.
        /// </summary>
        private LogConfigurationFactory()
        {
            this.loggingConfiguration = LogManager.Configuration ?? this.CreateConfig();
            this.BuildWebEventTarget();
        }

        /// <summary>
        ///   Gets the instance.
        /// </summary>
        /// <value> The instance. </value>
        internal static LogConfigurationFactory Instance
        {
            get { return instance; }
        }

        /// <summary>
        ///   Gets the rules.
        /// </summary>
        /// <value> The rules. </value>
        internal LoggingRule[] Rules
        {
            get { return this.rules.ToArray(); }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private void BuildLogServiceTargets()
        {
            FileTarget fileTarget = new FileTarget
            {
                FileName = "${basedir}/Logs/${shortdate}-${level}-internal-logger.log",
                Layout = "${longdate}|${level:uppercase=true}|${message}",
                AutoFlush = true,
                Name = LogServiceKey,
                MaxArchiveFiles = 20,
                CreateDirs = true,
                KeepFileOpen = true
            };
            this.AddTarget(LogServiceKey, fileTarget);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private void BuildWebEventTarget()
        {
            FileTarget webEventFileTarget = new FileTarget
            {
                FileName = "${basedir}/Logs/${shortdate}-WebEventProvider.log",
                Layout = "${message}",
                AutoFlush = true,
                Name = WebEventKey,
                MaxArchiveFiles = 20,
                CreateDirs = true,
                KeepFileOpen = true,
                ArchiveEvery = FileArchivePeriod.Day
            };
            this.AddTarget(WebEventKey, webEventFileTarget);
        }

        private void AddTarget(string key, Target target)
        {
            if (!this.targets.ContainsKey(key))
            {
                this.targets.Add(key, target);
            }
            else
            {
                this.targets[key] = target;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
        private void BuildDataBaseTarget(string loggerConnectionString)
        {
            DatabaseTarget databaseTarget = new DatabaseTarget
            {
                Name = LogServiceKey,
                ConnectionString = loggerConnectionString,
                CommandText =
                    "insert into LogEntry ([SiteId], [CreateDate], [Origin], [LogLevel], [Message], [Exception], [StackTrace]) values (@siteId, @createDate, @origin, @logLevel, @message, @exception, @stackTrace);",
                DBProvider = "mssql",
                KeepConnection = false
            };
            databaseTarget.Parameters.Add(new DatabaseParameterInfo
            {
                Name = "@siteId",
                Layout = "1"
            });

            databaseTarget.Parameters.Add(new DatabaseParameterInfo
            {
                Name = "@createDate",
                Layout = "${date}"
            });

            databaseTarget.Parameters.Add(new DatabaseParameterInfo
            {
                Name = "@origin",
                Layout = "${callsite}"
            });

            databaseTarget.Parameters.Add(new DatabaseParameterInfo
            {
                Name = "@logLevel",
                Layout = "${level}"
            });

            databaseTarget.Parameters.Add(new DatabaseParameterInfo
            {
                Name = "@message",
                Layout = "${message}"
            });

            databaseTarget.Parameters.Add(new DatabaseParameterInfo
            {
                Name = "@exception",
                Layout = "${exception:format=Message,StackTrace}"
            });

            databaseTarget.Parameters.Add(new DatabaseParameterInfo
            {
                Name = "@stackTrace",
                Layout = "${stackTrace}"
            });

            this.AddTarget(LogServiceKey, databaseTarget);
        }

        internal void SetLogLevelConfigeration(string loggerConnectionString, bool logToDatabase)
        {
            this.DetermineLogging(loggerConnectionString, logToDatabase);

            using (TimedLock.Lock(syncRoot))
            {
#if DEBUG
                this.EnableForDebug(this.targets[LogServiceKey], LogServiceKey, true);
                this.EnableForDebug(this.targets[WebEventKey], WebEventKey, true);
#else
                this.EnableForInfo(this.targets[LogServiceKey], LogServiceKey, true);
                this.EnableForInfo(this.targets[WebEventKey], WebEventKey, true);
#endif
            }

            LogManager.Configuration = this.loggingConfiguration;
            LogManager.ThrowExceptions = true;
            InternalLogger.LogFile = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\Internal.log";
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1500:VariableNamesShouldNotMatchFieldNames", MessageId = "logDefaultToDb")]
        private void DetermineLogging(string loggerConnectionString, bool logToDb)
        {
            if (logToDb)
            {
                this.BuildDataBaseTarget(loggerConnectionString);
            }
            else
            {
                this.BuildLogServiceTargets();
            }
        }

        private void EnableForDebug(Target target, string filter, bool addFilterRule = false)
        {
            LoggingRule rule = new LoggingRule(filter, LogLevel.Debug, target);
            if (addFilterRule)
            {
                rule.Filters.Add(new NotLoggerFilter(filter));
            }
            this.loggingConfiguration.LoggingRules.Add(rule);
        }

        private void EnableForInfo(Target target, string filter, bool addFilterRule = false)
        {
            LoggingRule rule = new LoggingRule(filter, LogLevel.Info, target);
            if (addFilterRule)
            {
                rule.Filters.Add(new NotLoggerFilter(filter));
            }
            this.loggingConfiguration.LoggingRules.Add(rule);
        }

        /// <summary>
        ///   Creates the config.
        /// </summary>
        /// <returns> </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
        private LoggingConfiguration CreateConfig()
        {
            return new LoggingConfiguration();
        }
    }
}