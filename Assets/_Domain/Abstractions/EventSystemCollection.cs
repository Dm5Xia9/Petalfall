using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EventSystemCollection
{
    private ConcurrentDictionary<string, List<Action<object>>> _subscribers = new ConcurrentDictionary<string, List<Action<object>>>();
    private object _lock = new object();

    public static EventSystemCollection Instance { get; private set; }
    static EventSystemCollection()
    {
        Instance = new EventSystemCollection();
    }

    public void Trigger(string eventId, object value)
    {
        lock (_lock)
        {
            if (_subscribers.ContainsKey(eventId))
            {
                foreach(var subscriber in _subscribers[eventId])
                {
                    subscriber(value);
                }
            }
        }
    }

    public void Subscribe(string eventId, Action<object> action)
    {
        lock (_lock)
        {
            if (_subscribers.ContainsKey(eventId))
            {
                _subscribers[eventId].Add(action);
            }
            else
            {
                _subscribers[eventId] = new List<Action<object>> { action };
            }
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
        var protocol = new EventProtocol<T>(name);
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
        var protocol = new EventProtocol(name);
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
