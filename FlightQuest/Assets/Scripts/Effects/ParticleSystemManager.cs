using UnityEngine;
using Zenject;

public sealed class ParticleSystemManager : MonoBehaviour, IFinishable
{
    [SerializeField] private ParticleSystem confettiEffect;

    public void ExecuteFinish() => confettiEffect.Play();
}
