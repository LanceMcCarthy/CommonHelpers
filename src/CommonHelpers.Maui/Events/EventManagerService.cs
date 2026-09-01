using CommonHelpers.Maui.Events.Exceptions;
using System.Reflection;
using System.Reflection.Emit;

namespace CommonHelpers.Maui.Events;

public static class EventManagerService
{
    internal static void AddEventHandler(in string eventName, in object? handlerTarget, in MethodInfo methodInfo, in Dictionary<string, List<Subscription>> eventHandlers)
    {
        var doesContainSubscriptions = eventHandlers.TryGetValue(eventName, out var targets);

        if (!doesContainSubscriptions || targets == null)
        {
            targets = [];
            eventHandlers.Add(eventName, targets);
        }

        targets.Add(handlerTarget == null ? new Subscription(null, methodInfo) : new Subscription(new WeakReference(handlerTarget), methodInfo));
    }

    internal static void RemoveEventHandler(in string eventName, in object? handlerTarget, in MemberInfo methodInfo, in Dictionary<string, List<Subscription>> eventHandlers)
    {
        var doesContainSubscriptions = eventHandlers.TryGetValue(eventName, out var subscriptions);

        if (!doesContainSubscriptions || subscriptions == null)
            return;

        for (var n = subscriptions.Count; n > 0; n--)
        {
            var current = subscriptions[n - 1];

            if (current.Subscriber?.Target != handlerTarget
                || current.Handler.Name != methodInfo?.Name)
            {
                continue;
            }

            subscriptions.Remove(current);
            break;
        }
    }

    internal static void HandleEvent(in string eventName, in object sender, in object eventArgs, in Dictionary<string, List<Subscription>> eventHandlers)
    {
        AddRemoveEvents(eventName, eventHandlers, out var toRaise);

        foreach (var t in toRaise)
        {
            try
            {
                var (instance, eventHandler) = t;
                if (eventHandler.IsLightweightMethod())
                {
                    var method = TryGetDynamicMethod(eventHandler);
                    method?.Invoke(instance, [sender, eventArgs]);
                }
                else
                {
                    eventHandler.Invoke(instance, [sender, eventArgs]);
                }
            }
            catch (TargetParameterCountException e)
            {
                throw new InvalidHandleEventException("Parameter count mismatch. If invoking an `event Action` use `HandleEvent(string eventName)` or if invoking an `event Action<T>` use `HandleEvent(object eventArgs, string eventName)`instead.", e);
            }
        }
    }

    internal static void HandleEvent(in string eventName, in object actionEventArgs, in Dictionary<string, List<Subscription>> eventHandlers)
    {
        AddRemoveEvents(eventName, eventHandlers, out var toRaise);

        foreach (var t in toRaise)
        {
            try
            {
                var (instance, eventHandler) = t;
                if (eventHandler.IsLightweightMethod())
                {
                    var method = TryGetDynamicMethod(eventHandler);
                    method?.Invoke(instance, [actionEventArgs]);
                }
                else
                {
                    eventHandler.Invoke(instance, [actionEventArgs]);
                }
            }
            catch (TargetParameterCountException e)
            {
                throw new InvalidHandleEventException("Parameter count mismatch. If invoking an `event EventHandler` use `HandleEvent(object? sender, TEventArgs eventArgs, string eventName)` or if invoking an `event Action` use `HandleEvent(string eventName)`instead.", e);
            }
        }
    }

    internal static void HandleEvent(in string eventName, in Dictionary<string, List<Subscription>> eventHandlers)
    {
        AddRemoveEvents(eventName, eventHandlers, out var toRaise);

        foreach (var t in toRaise)
        {
            try
            {
                var (instance, eventHandler) = t;
                if (eventHandler.IsLightweightMethod())
                {
                    var method = TryGetDynamicMethod(eventHandler);
                    method?.Invoke(instance, null);
                }
                else
                {
                    eventHandler.Invoke(instance, null);
                }
            }
            catch (TargetParameterCountException e)
            {
                throw new InvalidHandleEventException("Parameter count mismatch. If invoking an `event EventHandler` use `HandleEvent(object? sender, TEventArgs eventArgs, string eventName)` or if invoking an `event Action<T>` use `HandleEvent(object eventArgs, string eventName)`instead.", e);
            }
        }
    }

    private static void AddRemoveEvents(in string eventName, in Dictionary<string, List<Subscription>> eventHandlers, out List<(object Instance, MethodInfo EventHandler)> toRaise)
    {
        var toRemove = new List<Subscription>();
        toRaise = [];

        var doesContainEventName = eventHandlers.TryGetValue(eventName, out var target);

        if (!doesContainEventName || target == null) 
            return;

        foreach (var subscription in target)
        {
            var isStatic = subscription.Subscriber == null;

            if (isStatic)
            {
                toRaise.Add((null, subscription.Handler));
                continue;
            }

            var subscriber = subscription.Subscriber?.Target;

            if (subscriber == null)
                toRemove.Add(subscription);
            else
                toRaise.Add((subscriber, subscription.Handler));
        }

        foreach (var subscription in toRemove)
        {
            target.Remove(subscription);
        }
    }

    private static DynamicMethod TryGetDynamicMethod(in MethodInfo rtDynamicMethod)
    {
        var typeInfoRtDynamicMethod = typeof(DynamicMethod).GetTypeInfo().GetDeclaredNestedType("RTDynamicMethod");
        var typeRtDynamicMethod = typeInfoRtDynamicMethod?.AsType();

        if (typeInfoRtDynamicMethod != null && typeInfoRtDynamicMethod.IsAssignableFrom(rtDynamicMethod.GetType().GetTypeInfo()))
            return (DynamicMethod?)typeRtDynamicMethod?.GetRuntimeFields()?.FirstOrDefault(f => f?.Name is "m_owner")?.GetValue(rtDynamicMethod);
        else
            return null;
    }

    private static bool IsLightweightMethod(this MethodBase method)
    {
        var typeInfoRtDynamicMethod = typeof(DynamicMethod).GetTypeInfo().GetDeclaredNestedType("RTDynamicMethod");
        return method is DynamicMethod || (typeInfoRtDynamicMethod?.IsAssignableFrom(method.GetType().GetTypeInfo()) ?? false);
    }
}