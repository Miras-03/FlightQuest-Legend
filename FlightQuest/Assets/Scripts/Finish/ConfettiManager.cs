using UnityEngine;

public class ConfettiManager : MonoBehaviour, IFinishable
{
    [SerializeField] private ParticleSystem confetti;

    public void ExecuteFinish() => confetti.Play();
}
