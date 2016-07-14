using System;

namespace LEAP.Apps.ReadViewerDesktop
{
    public interface ILoggingService
    {
        void SetContext(string name);

        void Log(LogLevelTypes logLevel, object message);
        void Log(LogLevelTypes logLevel, Exception exception);
        void Log(LogLevelTypes logLevel, object message, Exception exception);
        void Log(LogLevelTypes logLevel, string format, params object[] args);
    }
}