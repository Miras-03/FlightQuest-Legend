using PlaneSection;
using UnityEngine;
using Zenject;

public sealed class DeathManager : MonoBehaviour
{
    private PlayerDeath playerDeath;

    private SpeedManager speedManager;
    private PlaneExplosion planeExplosion;
    private EngineSoundController engineSoundController;

    private bool injected = false;

    [Inject]
    public void Initialize(PrefabInitializationNotifier notifier, PlayerDeath playerDeath)
    {
        this.playerDeath = playerDeath;
        notifier.OnPrefabInitialized += InjectAfterDelay;
    }

    private void InjectAfterDelay()
    {
        if (!injected)
        {
            injected = true;

            speedManager = FindObjectOfType<SpeedManager>();
            planeExplosion = FindObjectOfType<PlaneExplosion>();
            engineSoundController = FindObjectOfType<EngineSoundController>();

            playerDeath.AddObservers(speedManager);
            playerDeath.AddObservers(planeExplosion);
            playerDeath.AddObservers(engineSoundController);
        }
    }

    private void OnDisable() => playerDeath.RemoveObservers();
}
