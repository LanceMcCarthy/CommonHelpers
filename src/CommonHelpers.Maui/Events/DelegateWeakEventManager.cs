﻿using System.Reflection;
using System.Runtime.CompilerServices;

namespace CommonHelpers.Maui.Events;

public class DelegateWeakEventManager
{
    readonly Dictionary<string, List<Subscription>> eventHandlers = new Dictionary<string, List<Subscription>>();

    /// <summary>
    /// Adds the event handler
    /// </summary>
    /// <param name="handler">Handler</param>
    /// <param name="eventName">Event name</param>
    public void AddEventHandler(Delegate? handler, [CallerMemberName] string eventName = "")
    {
        if (string.IsNullOrWhiteSpace(eventName))
            throw new ArgumentNullException(nameof(eventName));

        if (handler == null)
            throw new ArgumentNullException(nameof(handler));

        var methodInfo = handler.GetMethodInfo() ?? throw new NullReferenceException("Could not locate MethodInfo");

        EventManagerService.AddEventHandler(eventName, handler.Target, methodInfo, eventHandlers);
    }

    /// <summary>
    /// Removes the event handler.
    /// </summary>
    /// <param name="handler">Handler</param>
    /// <param name="eventName">Event name</param>
    public void RemoveEventHandler(Delegate? handler, [CallerMemberName] string eventName = "")
    {
        if (string.IsNullOrWhiteSpace(eventName))
            throw new ArgumentNullException(nameof(eventName));

        if (handler == null)
            throw new ArgumentNullException(nameof(handler));

        var methodInfo = handler.GetMethodInfo() ?? throw new NullReferenceException("Could not locate MethodInfo");

        EventManagerService.RemoveEventHandler(eventName, handler.Target, methodInfo, eventHandlers);
    }

    /// <summary>
    /// Invokes the event EventHandler
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="eventArgs">Event arguments</param>
    /// <param name="eventName">Event name</param>
    public void HandleEvent(object? sender, object eventArgs, string eventName) => RaiseEvent(sender, eventArgs, eventName);

    /// <summary>
    /// Invokes the event Action
    /// </summary>
    /// <param name="eventName">Event name</param>
    public void HandleEvent(string eventName) => RaiseEvent(eventName);

    /// <summary>
    /// Invokes the event EventHandler
    /// </summary>
    /// <param name="sender">Sender</param>
    /// <param name="eventArgs">Event arguments</param>
    /// <param name="eventName">Event name</param>
    public void RaiseEvent(object? sender, object eventArgs, string eventName) =>
        EventManagerService.HandleEvent(eventName, sender, eventArgs, eventHandlers);

    /// <summary>
    /// Invokes the event Action
    /// </summary>
    /// <param name="eventName">Event name</param>
    public void RaiseEvent(string eventName) => EventManagerService.HandleEvent(eventName, eventHandlers);
}