using PlaneSection;
using UnityEngine;
using Zenject;

public sealed class FinishManager : MonoBehaviour
{
    [Header("FinishObservers")]
    [SerializeField] private ParticleSystemManager particleSystemManager;
    [SerializeField] private UIManager managerUI;
    [SerializeField] private SpeedManager speedManager;
    [SerializeField] private LevelManager levelManager;

    private FinishLine finishLine;

    [Inject]
    public void Constructor(FinishLine finishLine)
    {
        this.finishLine = finishLine;

        finishLine.AddObservers(particleSystemManager);
        finishLine.AddObservers(managerUI);
        finishLine.AddObservers(speedManager);
        finishLine.AddObservers(levelManager);
    }

    private void OnDisable() => finishLine.RemoveObservers();
}