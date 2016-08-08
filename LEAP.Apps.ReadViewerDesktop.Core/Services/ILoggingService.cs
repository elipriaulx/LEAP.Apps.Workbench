using LEAP.Apps.ReadViewerDesktop.Core.Components;

namespace LEAP.Apps.ReadViewerDesktop.Core.Services
{
    public interface ILoggingService
    {
        ILogger CreateLogger(string name = "");
    }
}