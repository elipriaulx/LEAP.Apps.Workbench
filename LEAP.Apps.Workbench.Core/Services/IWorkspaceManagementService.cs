using System;
using System.Collections.Generic;
using LEAP.Apps.Workbench.Core.Components;

namespace LEAP.Apps.Workbench.Core.Services
{
    public interface IWorkspaceManagementService
    {
        IWorkspaceGroupManager RegisterLoader(string extension, Guid id, string name, Func<string, object> fileLoader);
        void UnregisterLoader(string extension, IWorkspaceGroupManager fileLoader);

        IWorkspaceGroupManager GetLoader(string extension, Guid loaderId);

        IEnumerable<IWorkspaceGroupManager> GetLoaders(string extension);
        IReadOnlyDictionary<string, IReadOnlyList<IWorkspaceGroupManager>> GetAllLoaders();
    }
}