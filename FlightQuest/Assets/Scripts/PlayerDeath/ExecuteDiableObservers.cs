using System.Collections.Generic;
using UnityEngine;
using Zenject;

public sealed class ExecuteDiableObservers : MonoBehaviour
{
    [SerializeField] private ParticleSystem smoke;

    private PlayerDeath playerDeath;

    [Inject]
    public void Construct(PlayerDeath playerDeath) => this.playerDeath = playerDeath;

    private readonly HashSet<string> excludedTags = new HashSet<string> { "Point", "Finish", "Ground", "LosePoint", "EnterPoint", "Landing" };

    private void OnTriggerEnter(Collider other)
    {
        if (!excludedTags.Contains(other.tag))
            playerDeath.NotifyObserversAboutDeath();
    }
}
