using UnityEngine;
using UnityEngine.Events;

public abstract class GameEventBase : ScriptableObject {}

public class GameEvent : GameEventBase
{
    private event UnityAction listeners;

    public void Raise()
    {
        listeners?.Invoke();
    }

    public void RegisterListener(UnityAction listener)
    {
        listeners += listener;
    }

    public void UnregisterListener(UnityAction listener)
    {
        listeners -= listener;
    }
}

public class GameEventString : GameEventBase
{
    private event UnityAction<string> listeners;

    public void Raise(string value)
    {
        listeners?.Invoke(value);
    }

    public void RegisterListener(UnityAction<string> listener)
    {
        listeners += listener;
    }

    public void UnregisterListener(UnityAction<string> listener)
    {
        listeners -= listener;
    }
}
