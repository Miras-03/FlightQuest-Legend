using UnityEngine;
using Zenject;

public sealed class ExecuteDiableObservers : MonoBehaviour
{
    private PlayerDeath playerDeath;
    private Plane plane;

    [Inject]
    public void Construct(Plane plane, PlayerDeath playerDeath)
    {
        this.plane = plane;
        this.playerDeath = playerDeath;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!plane.isLandingGearRemoved || !other.CompareTag("Point") && !other.CompareTag("Finish"))
            playerDeath.NotifyObserversAboutDeath();
    }
}
