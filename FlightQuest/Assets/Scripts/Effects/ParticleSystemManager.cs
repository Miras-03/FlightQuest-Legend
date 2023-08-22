using UnityEngine;
using Zenject;

public class ParticleSystemManager : MonoBehaviour, IFinishable, IDieable
{
    private ParticleSystem explosionEffect;
    [SerializeField] private ParticleSystem confettiEffect;

    [Inject]
    public void Construction(ParticleSystem explosionEffect) => this.explosionEffect = explosionEffect;

    public void ExecuteExplode() => confettiEffect.Play();
    public void ExecuteFinish() => explosionEffect.Play();
}
