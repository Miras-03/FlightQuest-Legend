using PlaneSection;
using UnityEngine;
using Zenject;

public class AccelerationManager : MonoBehaviour
{
    private PlaneLevelAcceleration accelerationLevel;
    [SerializeField] private PropellerRotate propellerRotate;

    [Inject]
    public void Construct(PlaneLevelAcceleration accelerationLevel)
    {
        this.accelerationLevel = accelerationLevel;
        this.accelerationLevel.AddOvserver(propellerRotate);
    }

    private void OnDisable() => accelerationLevel.RemoveObserver();
}
