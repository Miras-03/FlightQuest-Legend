using System.Collections.Generic;

namespace PlaneSection
{
    public sealed class PlaneLevelAcceleration
    {
        private Dictionary<int, IAccelerationObserver> accelerationObservers = new Dictionary<int, IAccelerationObserver>();
        public float AccelerationLevel { set => NotifyObservers(value); }

        public void AddOvserver(IAccelerationObserver observer) => accelerationObservers.Add(accelerationObservers.Count,observer);
        public void RemoveObserver() => accelerationObservers.Clear();

        private void NotifyObservers(float accelerationLevel)
        {
            foreach (var observer in accelerationObservers.Values)
                observer.GetAccelerationLevel(accelerationLevel);
        }
    }
}