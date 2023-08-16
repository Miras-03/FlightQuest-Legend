using System.Collections.Generic;

namespace PlaneSection
{
    public sealed class PlaneLevelAcceleration
    {
        private HashSet<IAccelerationObserver> accelerationObservers = new HashSet<IAccelerationObserver>();
        public float AccelerationLevel { set => NotifyObservers(value); }

        public void AddOvserver(IAccelerationObserver observer) => accelerationObservers.Add(observer);
        public void RemoveObserver() => accelerationObservers.Clear();

        private void NotifyObservers(float accelerationLevel)
        {
            foreach (var observer in accelerationObservers)
                observer.GetAccelerationLevel(accelerationLevel);
        }
    }
}