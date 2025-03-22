using UnityEngine.Events;

public interface IBootDependency
{
    UnityEvent OnReady { get; }
}