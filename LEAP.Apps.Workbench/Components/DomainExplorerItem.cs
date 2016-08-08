using System;
using System.Collections.Generic;
using LEAP.Apps.Workbench.Core.Components;

namespace LEAP.Apps.Workbench.Components
{
    public class DomainExplorerItem : IDomainExplorerItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Action SelectionAction { get; set; }
        public IEnumerable<IDomainExplorerItem> ChildItems { get; set; }
    }
}
