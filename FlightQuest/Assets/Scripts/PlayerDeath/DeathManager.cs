using PlaneSection;
using UnityEngine;
using Zenject;

public sealed class DeathManager : MonoBehaviour
{
    private PlayerDeath playerDeath;

    private SpeedManager speedManager;
    private PlaneExplosion planeExplosion;

    private bool injected = false;

    [Inject]
    public void Initialize(PrefabInitializationNotifier notifier) => 
        notifier.OnPrefabInitialized += InjectAfterDelay;

    private void InjectAfterDelay()
    {
        if (!injected)
        {
            injected = true;

            speedManager = FindObjectOfType<SpeedManager>();
            planeExplosion = FindObjectOfType<PlaneExplosion>();

            playerDeath.AddObservers(speedManager);
            playerDeath.AddObservers(planeExplosion);
        }
    }

    [Inject]
    public void Constructor(PlayerDeath playerDeath) => this.playerDeath = playerDeath;

    private void OnDisable() => playerDeath.RemoveObservers();
}
