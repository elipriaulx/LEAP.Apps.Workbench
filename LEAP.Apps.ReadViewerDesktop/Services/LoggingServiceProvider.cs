using System;
using LEAP.Apps.ReadViewerDesktop.Core.Services;
using NLog;
using NLog.Config;
using NLog.Targets;
using ILogger = LEAP.Apps.ReadViewerDesktop.Core.Components.ILogger;

namespace LEAP.Apps.ReadViewerDesktop.Services
{
    public class LoggingServiceProvider : ILoggingService
    {
        public LoggingServiceProvider()
        {
            
        }
        
        public ILogger CreateLogger(string name = "Default")
        {
            return new NLogLogger(name);
        }

        public class NLogLogger : ILogger
        {
            private readonly Logger _logger;

            internal NLogLogger(string name)
            {
                _logger = LogManager.GetLogger(name);

                var config = new LoggingConfiguration();

                var fileTarget = new FileTarget();
                config.AddTarget("file", fileTarget);

                fileTarget.FileName = "${basedir}/application.log";
                fileTarget.Layout = $"[{name}: ${{level}} @ ${{longdate}}] ${{message}}";

                var rule2 = new LoggingRule("*", NLog.LogLevel.Trace, fileTarget);
                config.LoggingRules.Add(rule2);

                LogManager.Configuration = config;
            }
     
            public void Info(object message)
            {
                _logger.Info(message);
            }

            public void Warning(object message)
            {
                _logger.Warn(message);
            }

            public void Warning(Exception exception)
            {
                _logger.Warn(exception);
            }

            private void Debug(string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
            {
                //_logger.Debug($"Debug in [{memberName}] of [{sourceFilePath}], line [{sourceLineNumber}]: ");
            }

            public void Debug(object message, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
            {
                Debug(memberName, sourceFilePath, sourceLineNumber);
                _logger.Debug(message);
            }

            public void Debug(Exception exception, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
            {
                Debug(memberName, sourceFilePath, sourceLineNumber);
                _logger.Debug(exception);
            }

            private void Trace(string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
            {
                //_logger.Trace($"Trace in [{memberName}] of [{sourceFilePath}], line [{sourceLineNumber}]: ");
            }

            public void Trace(object message, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
            {
                Trace(memberName, sourceFilePath, sourceLineNumber);
                _logger.Trace(message);
            }

            public void Trace(Exception exception, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
            {
                Trace(memberName, sourceFilePath, sourceLineNumber);
                _logger.Trace(exception);
            }

            private void Error(string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
            {
                //_logger.Error($"Error in [{memberName}] of [{sourceFilePath}], line [{sourceLineNumber}]: ");
            }

            public void Error(object message, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
            {
                Error(memberName, sourceFilePath, sourceLineNumber);
                _logger.Error(message);
            }

            public void Error(Exception exception, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
            {
                Error(memberName, sourceFilePath, sourceLineNumber);
                _logger.Error(exception);
            }

            private void Critical(string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
            {
                //_logger.Error($"Critical in [{memberName}] of [{sourceFilePath}], line [{sourceLineNumber}]: ");
            }

            public void Critical(object message, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
            {
                Critical(memberName, sourceFilePath, sourceLineNumber);
                _logger.Fatal(message);
            }

            public void Critical(Exception exception, string memberName = "", string sourceFilePath = "", int sourceLineNumber = 0)
            {
                Critical(memberName, sourceFilePath, sourceLineNumber);
                _logger.Fatal(exception);
            }
        }
    }
}
