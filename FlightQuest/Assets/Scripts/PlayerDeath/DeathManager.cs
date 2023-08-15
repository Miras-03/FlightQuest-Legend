using PlaneSection;
using UnityEngine;
using Zenject;

public sealed class DeathManager : MonoBehaviour
{
    private PlayerDeath playerDeath;

    [Header("DeathObservers")]
    [SerializeField] private PlaneExplosion planeExplosion;
    [SerializeField] private ParticleSystemManager particleSystemManager;
    [SerializeField] private SpeedManager speedManager;

    [Inject]
    public void Constructor(PlayerDeath playerDeath)
    {
        this.playerDeath = playerDeath;

        playerDeath.AddObservers(planeExplosion);
        playerDeath.AddObservers(speedManager);
        playerDeath.AddObservers(particleSystemManager);
    }

    private void OnDisable() => playerDeath.RemoveObservers();
}
