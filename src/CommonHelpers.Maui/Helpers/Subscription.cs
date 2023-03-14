using System.Reflection;

namespace CommonHelpers.Maui.Helpers;

public readonly struct Subscription
{
    public WeakReference? Subscriber { get; }

    public MethodInfo Handler { get; }

    public Subscription(WeakReference? subscriber, MethodInfo handler)
    {
        Subscriber = subscriber;
        Handler = handler ?? throw new ArgumentNullException(nameof(handler));
    }
}