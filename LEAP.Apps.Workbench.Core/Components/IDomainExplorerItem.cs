using System;
using System.Collections.Generic;

namespace LEAP.Apps.Workbench.Core.Components
{
    public interface IDomainExplorerItem
    {
        Guid Id { get; }
        string Name { get; }
        string Description { get; }
        Action SelectionAction { get; }
        IEnumerable<IDomainExplorerItem> ChildItems { get; }
    }
}