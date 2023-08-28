using UnityEngine;
using Zenject;

public sealed class ParticleSystemManager : MonoBehaviour, IFinishable
{
    [SerializeField] private ParticleSystem confettiEffect;
    private AudioSource[] confettiSound;

    [Inject] 
    public void Constructor(AudioSource[] confettiSound) => this.confettiSound = confettiSound;

    public void ExecuteFinish()
    {
        confettiSound[3].Play();
        confettiEffect.Play();
    }
}
