using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventBase : ScriptableObject, IGameEvent
{
    protected readonly List<Action> actions = new List<Action>();

    protected readonly List<IGameEventListener> listeners = new List<IGameEventListener>();


    public void Raise()
    {
        for (var i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised();
        for (var i = actions.Count - 1; i >= 0; i--)
            actions[i].Invoke();
    }

    public void AddListener(IGameEventListener listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void RemoveListener(IGameEventListener listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }

    public void RemoveAll()
    {
        listeners.Clear();
    }

    public void AddListener(Action action)
    {
        if (!actions.Contains(action))
            actions.Add(action);
    }

    public void RemoveListener(Action action)
    {
        if (actions.Contains(action))
            actions.Remove(action);
    }
}

public abstract class GameEventBase<T> : ScriptableObject, IGameEvent<T>
{
    protected readonly List<Action<T>> actions = new List<Action<T>>();
    protected readonly List<IGameEventListener<T>> listeners = new List<IGameEventListener<T>>();

    public void Raise(T value)
    {
        for (var i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(value);
        for (var i = actions.Count - 1; i >= 0; i--)
            actions[i].Invoke(value);
    }

    public void AddListener(IGameEventListener<T> listener)
    {
        if (!listeners.Contains(listener))
            listeners.Add(listener);
    }

    public void RemoveListener(IGameEventListener<T> listener)
    {
        if (listeners.Contains(listener))
            listeners.Remove(listener);
    }

    public void RemoveAll()
    {
        listeners.Clear();
    }

    public void AddListener(Action<T> action)
    {
        if (!actions.Contains(action))
            actions.Add(action);
    }

    public void RemoveListener(Action<T> action)
    {
        if (actions.Contains(action))
            actions.Remove(action);
    }

    public override string ToString()
    {
        return "GameEvent<" + typeof(T) + ">";
    }
}

public interface IGameEventListener
{
    void OnEventRaised();
}

public interface IGameEventListener<T>
{
    void OnEventRaised(T value);
}

public interface IGameEvent<T>
{
    void Raise(T value);
    void AddListener(IGameEventListener<T> listener);
    void RemoveListener(IGameEventListener<T> listener);
    void RemoveAll();
}

public interface IGameEvent
{
    void Raise();
    void AddListener(IGameEventListener listener);
    void RemoveListener(IGameEventListener listener);
    void RemoveAll();
}