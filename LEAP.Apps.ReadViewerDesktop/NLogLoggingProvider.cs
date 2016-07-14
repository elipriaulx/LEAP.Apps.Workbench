using System;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace LEAP.Apps.ReadViewerDesktop
{
    public class NLogLoggingProvider : ILoggingService
    {
        private Logger _logger;

        public NLogLoggingProvider()
        {
            _logger = LogManager.GetCurrentClassLogger();
            
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget();
            config.AddTarget("file", fileTarget);

            fileTarget.FileName = "${basedir}/application.log";
            fileTarget.Layout = "${longdate} - ${level} - ${message}";

            var rule2 = new LoggingRule("*", LogLevel.Debug, fileTarget);
            config.LoggingRules.Add(rule2);

            LogManager.Configuration = config;
        }

        public void SetContext(string name)
        {
            _logger = LogManager.GetLogger(name);
        }

        public void Log(LogLevelTypes logLevel, object message)
        {
            switch (logLevel)
            {
                case LogLevelTypes.Unspecified:
                    throw new NotImplementedException();

                case LogLevelTypes.Trace:
                    _logger.Trace(message);
                    break;

                case LogLevelTypes.Debug:
                    _logger.Debug(message);
                    break;

                case LogLevelTypes.Info:
                    _logger.Info(message);
                    break;

                case LogLevelTypes.Warning:
                    _logger.Warn(message);
                    break;

                case LogLevelTypes.Error:
                    _logger.Error(message);
                    break;

                case LogLevelTypes.Critical:
                    _logger.Fatal(message);
                    break;

                default:
                    throw new NotImplementedException();
            }
            ;
        }

        public void Log(LogLevelTypes logLevel, Exception exception)
        {
            switch (logLevel)
            {
                case LogLevelTypes.Unspecified:
                    throw new NotImplementedException();

                case LogLevelTypes.Trace:
                    _logger.Trace(exception);
                    break;

                case LogLevelTypes.Debug:
                    _logger.Debug(exception);
                    break;

                case LogLevelTypes.Info:
                    _logger.Info(exception);
                    break;

                case LogLevelTypes.Warning:
                    _logger.Warn(exception);
                    break;

                case LogLevelTypes.Error:
                    _logger.Error(exception);
                    break;

                case LogLevelTypes.Critical:
                    _logger.Fatal(exception);
                    break;

                default:
                    throw new NotImplementedException();
            }
            ;
        }

        public void Log(LogLevelTypes logLevel, object message, Exception exception)
        {
            switch (logLevel)
            {
                case LogLevelTypes.Unspecified:
                    throw new NotImplementedException();

                case LogLevelTypes.Trace:
                    _logger.Trace(message);
                    _logger.Trace(exception);
                    break;

                case LogLevelTypes.Debug:
                    _logger.Debug(message);
                    _logger.Debug(exception);
                    break;

                case LogLevelTypes.Info:
                    _logger.Info(message);
                    _logger.Info(exception);
                    break;

                case LogLevelTypes.Warning:
                    _logger.Warn(message);
                    _logger.Warn(exception);
                    break;

                case LogLevelTypes.Error:
                    _logger.Error(message);
                    _logger.Error(exception);
                    break;

                case LogLevelTypes.Critical:
                    _logger.Fatal(message);
                    _logger.Fatal(exception);
                    break;

                default:
                    throw new NotImplementedException();
            }
            ;
        }

        public void Log(LogLevelTypes logLevel, string format, params object[] args)
        {
            switch (logLevel)
            {
                case LogLevelTypes.Unspecified:
                    throw new NotImplementedException();

                case LogLevelTypes.Trace:
                    _logger.Trace(format, args);
                    break;

                case LogLevelTypes.Debug:
                    _logger.Debug(format, args);
                    break;

                case LogLevelTypes.Info:
                    _logger.Info(format, args);
                    break;

                case LogLevelTypes.Warning:
                    _logger.Warn(format, args);
                    break;

                case LogLevelTypes.Error:
                    _logger.Error(format, args);
                    break;

                case LogLevelTypes.Critical:
                    _logger.Fatal(format, args);
                    break;

                default:
                    throw new NotImplementedException();
            };
        }
    }
}
