using System.Collections.Generic;

public sealed class FinishLine
{
    private Dictionary<int, IFinishable> observers = new Dictionary<int, IFinishable>();

    public void AddObservers(IFinishable observer) => observers.Add(observers.Count, observer);
    public void RemoveObservers() => observers.Clear();

    public void NotifyObserversAboutFinish()
    {
        foreach (IFinishable observer in observers.Values) 
            observer.ExecuteFinish();
    }
}
