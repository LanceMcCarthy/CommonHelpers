using System.Reflection;

namespace CommonHelpers.Maui.Events;

public readonly struct Subscription(WeakReference subscriber, MethodInfo handler)
{
    public WeakReference Subscriber { get; } = subscriber;

    public MethodInfo Handler { get; } = handler ?? throw new ArgumentNullException(nameof(handler));
}