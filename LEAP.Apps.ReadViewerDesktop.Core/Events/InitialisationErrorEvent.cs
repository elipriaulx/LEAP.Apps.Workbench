using System;
using Prism.Events;

namespace LEAP.Apps.ReadViewerDesktop.Core.Events
{
    public class InitialisationErrorEvent : PubSubEvent<Exception>
    {
    }
}
