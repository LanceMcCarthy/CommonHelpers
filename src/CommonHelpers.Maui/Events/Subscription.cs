using System.Reflection;

namespace CommonHelpers.Maui.Events;

public readonly record struct Subscription(WeakReference Subscriber, MethodInfo Handler)
{
    public WeakReference Subscriber { get; } = Subscriber;

    public MethodInfo Handler { get; } = Handler ?? throw new ArgumentNullException(nameof(Handler));
}