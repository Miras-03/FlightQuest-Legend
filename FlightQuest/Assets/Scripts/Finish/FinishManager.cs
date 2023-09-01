using PlaneSection;
using UnityEngine;
using Zenject;

public sealed class FinishManager : MonoBehaviour
{
    private FinishLine finishLine;

    [Header("FinishObservers")]
    [SerializeField] private LevelManager levelManager;
    private SpeedManager speedManager;
    private UIManager managerUI;
    private ParticleSystemManager particleSystemManager;

    [SerializeField] private CanisterSpawner canisterSpawner;
    [SerializeField] private ShipSpawner shipSpawner;

    private bool injected;

    [Inject]
    public void Initialize(PrefabInitializationNotifier notifier, FinishLine finishLine)
    {
        this.finishLine = finishLine;
        notifier.OnPrefabInitialized += InjectAfterDelay;
    }

    private void InjectAfterDelay()
    {
        if (!injected)
        {
            injected = true;

            particleSystemManager = FindObjectOfType<ParticleSystemManager>();
            managerUI = FindObjectOfType<UIManager>();
            speedManager = FindObjectOfType<SpeedManager>();
            levelManager = FindObjectOfType<LevelManager>();

            finishLine.AddObservers(particleSystemManager);
            finishLine.AddObservers(managerUI);
            finishLine.AddObservers(speedManager);
            finishLine.AddObservers(levelManager);
            finishLine.AddObservers(canisterSpawner);
            finishLine.AddObservers(shipSpawner);
        }
    }

    private void OnDisable() => finishLine.RemoveObservers();
}