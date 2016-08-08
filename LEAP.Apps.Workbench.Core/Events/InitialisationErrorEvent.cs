using System;
using Prism.Events;

namespace LEAP.Apps.Workbench.Core.Events
{
    public class InitialisationErrorEvent : PubSubEvent<Exception>
    {
    }
}
