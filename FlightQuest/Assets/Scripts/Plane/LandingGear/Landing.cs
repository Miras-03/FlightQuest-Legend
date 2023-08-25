using System.Collections.Generic;

public class Landing
{
    private HashSet<ILandable> observers = new HashSet<ILandable>();

    public void AddObservers(ILandable observer) => observers.Add(observer);
    public void RemoveObservers() => observers.Clear();

    public void NotifyObserversAboutLand()
    {
        foreach (ILandable observer in observers)
            observer.ExecuteLand();
    }
}
