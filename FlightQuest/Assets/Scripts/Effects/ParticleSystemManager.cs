using UnityEngine;

public class ParticleSystemManager : MonoBehaviour, IFinishable, IDieable
{
    [SerializeField] private ParticleSystem[] effects;

    public void ExecuteExplode() => effects[1].Play();
    public void ExecuteFinish() => effects[0].Play();

}
