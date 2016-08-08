using System;
using LEAP.Core.Exceptions;

namespace LEAP.Apps.Workbench.Core.Exceptions
{
    public class LeapWorkbenchException : LeapException
    {
        public LeapWorkbenchException()
        {

        }

        public LeapWorkbenchException(string message) : base(message)
        {

        }

        public LeapWorkbenchException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
