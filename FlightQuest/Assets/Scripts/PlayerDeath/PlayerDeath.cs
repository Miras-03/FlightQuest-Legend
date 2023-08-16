using System.Collections.Generic;

public sealed class PlayerDeath
{
    private HashSet<IDieable> observers = new HashSet<IDieable>();

    public void AddObservers(IDieable observer) => observers.Add(observer);
    public void RemoveObservers() => observers.Clear();

    public void NotifyObserversAboutDeath()
    {
        foreach (IDieable observer in observers)
            observer.ExecuteExplode();
    }
}
