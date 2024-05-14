using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

public class EventSystemCollection
{
    private ConcurrentDictionary<string, List<Action<object>>> _subscribers = new();
    private object _lock = new();

    static EventSystemCollection()
    {
        Instance = new EventSystemCollection();
    }

    public static EventSystemCollection Instance { get; private set; }

    public void Trigger(string eventId, object value)
    {
        lock (_lock)
        {
            if (_subscribers.ContainsKey(eventId))
            {
                foreach (Action<object> subscriber in _subscribers[eventId])
                    subscriber(value);
            }
        }
    }

    public void Subscribe(string eventId, Action<object> action)
    {
        lock (_lock)
        {
            if (_subscribers.ContainsKey(eventId))
                _subscribers[eventId].Add(action);
            else
                _subscribers[eventId] = new() { action };
        }
    }



}

public class EventProtocol<T>
{
    private readonly string _name;

    private EventProtocol(string name)
    {
        _name = name;
    }

    public static EventProtocol<T> Create(string name)
    {
        EventProtocol<T> protocol = new(name);
        return protocol;
    }

    public void Subscribe(Action<T> action)
    {
        EventSystemCollection.Instance.Subscribe(_name, p => action((T)p));
    }

    public void Trigger(T value)
    {
        EventSystemCollection.Instance.Trigger(_name, value);
    }
}

public class EventProtocol
{
    private readonly string _name;

    private EventProtocol(string name)
    {
        _name = name;
    }

    public static EventProtocol Create(string name)
    {
        EventProtocol protocol = new(name);
        return protocol;
    }

    public void Subscribe(Action action)
    {
        EventSystemCollection.Instance.Subscribe(_name, p => action());
    }

    public void Trigger()
    {
        EventSystemCollection.Instance.Trigger(_name, null);
    }
}
