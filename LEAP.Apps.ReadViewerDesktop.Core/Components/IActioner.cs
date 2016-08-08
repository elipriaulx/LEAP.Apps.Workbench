using System;
using System.Drawing;

namespace LEAP.Apps.ReadViewerDesktop.Core.Components
{
    public interface IActioner
    {
        Guid Id { get; }

        string Name { get; }
        string Description { get; }

        Image Icon { get; }
    }

    public interface IActionComponent<in T> : IActioner
    {
        Action<T> Operation { get; }
    }

    public interface IActionComponent<in T, out TO> : IActioner
    {
        Func<T, TO> Operation { get; }
    }
}