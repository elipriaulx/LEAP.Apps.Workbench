using System;
using System.Collections.Generic;
using LEAP.Apps.ReadViewerDesktop.Core.Components;

namespace LEAP.Apps.ReadViewerDesktop.Core.Services
{
    public interface IWorkspaceIoService
    {
        IFileLoader RegisterLoader(string extension, Guid id, string name, Func<string, object> fileLoader);
        void UnregisterLoader(string extension, IFileLoader fileLoader);

        IFileLoader GetLoader(string extension, Guid loaderId);

        IEnumerable<IFileLoader> GetLoaders(string extension);
        IReadOnlyDictionary<string, IReadOnlyList<IFileLoader>> GetAllLoaders();
    }
}