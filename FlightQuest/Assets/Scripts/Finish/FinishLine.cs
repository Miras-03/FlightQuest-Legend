using System.Collections.Generic;

public sealed class FinishLine
{
    private HashSet<IFinishable> observers = new HashSet<IFinishable>();

    public void AddObservers(IFinishable observer) => observers.Add(observer);
    public void RemoveObservers() => observers.Clear();

    public void NotifyObserversAboutFinish()
    {
        foreach (IFinishable observer in observers) 
            observer.ExecuteFinish();
    }
}
