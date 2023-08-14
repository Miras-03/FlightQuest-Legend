using PlaneSection;
using UnityEngine;
using Zenject;

public sealed class DeathManager : MonoBehaviour
{
    private PlayerDeath playerDeath;

    [Header("DeathObservers")]
    [SerializeField] private PlaneExplosion planeExplosion;
    [SerializeField] private ControlManager controlManager;

    [Inject]
    public void Constructor(PlayerDeath playerDeath)
    {
        this.playerDeath = playerDeath;

        playerDeath.AddObservers(planeExplosion);
        playerDeath.AddObservers(controlManager);
    }

    private void OnDisable() => playerDeath.RemoveObservers();
}
