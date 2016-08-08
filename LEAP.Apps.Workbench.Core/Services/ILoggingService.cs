using LEAP.Apps.Workbench.Core.Components;

namespace LEAP.Apps.Workbench.Core.Services
{
    public interface ILoggingService
    {
        ILogger CreateLogger(string name = "");
    }
}