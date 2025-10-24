using System;
using System.Collections.Generic;
using Godot;

namespace BasicShmup.Events;

public partial class MessageBroker : Node
{
    public static MessageBroker Instance { get; } = new();

    private readonly Dictionary<Type, List<object>> _eventHandlers = new();

    private MessageBroker()
    {
    }

    public void RegisterEventHandler<TEvent>(IEventHandler<TEvent> eventHandler)
    {
        var eventType = typeof(TEvent);
        _eventHandlers.TryAdd(eventType, []);

        var handlers = _eventHandlers[eventType];

        handlers.Add(eventHandler);
    }

    public void RemoveEventHandler<TEvent>(IEventHandler<TEvent> eventHandler)
    {
        var eventType = typeof(TEvent);
        _eventHandlers.TryGetValue(eventType, out var eventHandlers);

        if (eventHandlers == null)
            return;

        var index = eventHandlers.IndexOf(eventHandlers);
        var eventHandlerIsRegistered = index >= 0;
        if (!eventHandlerIsRegistered)
            return;

        eventHandlers.RemoveAt(index);
        if (eventHandlers.Count == 0)
            _eventHandlers.Remove(eventType);
    }

    public void Send<TEvent>(TEvent @event)
    {
        var eventType = typeof(TEvent);

        _eventHandlers.TryGetValue(eventType, out var eventHandlers);
        if (eventHandlers == null)
            return;

        foreach (var eventHandler in eventHandlers)
            ((IEventHandler<TEvent>)eventHandler).Handle(@event);
    }
}
