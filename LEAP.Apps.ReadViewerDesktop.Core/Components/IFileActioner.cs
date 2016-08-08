using System;

namespace LEAP.Apps.ReadViewerDesktop.Core.Components
{
    public interface IFileActioner
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }

        Action<object> ActionFile { get; }
    }
}