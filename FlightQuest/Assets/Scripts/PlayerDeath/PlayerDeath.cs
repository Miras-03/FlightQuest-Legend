using System.Collections.Generic;

public sealed class PlayerDeath
{
    private Dictionary<int, IDieable> observers = new Dictionary<int, IDieable>();

    public void AddObservers(IDieable observer) => observers.Add(observers.Count, observer);
    public void RemoveObservers() => observers.Clear();

    public void NotifyObserversAboutDeath()
    {
        foreach (IDieable observer in observers.Values)
            observer.ExecuteExplode();
    }
}
