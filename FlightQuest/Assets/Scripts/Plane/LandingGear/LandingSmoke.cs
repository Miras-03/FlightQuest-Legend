using UnityEngine;
using Zenject;

public sealed class LandingSmoke : MonoBehaviour
{
    [SerializeField] private ParticleSystem smoke;
    private AudioSource[] touchLand;

    [Inject]
    public void Constructor(AudioSource[] touchLand) => this.touchLand = touchLand;

    private void OnCollisionEnter(Collision collision)
    {
        float impactForceThreshold = 10f;
        Vector3 relativeVelocity = collision.relativeVelocity;
        float impactForce = relativeVelocity.magnitude;

        if (impactForce > impactForceThreshold)
        {
            touchLand[2].Play();
            smoke.Play();
        }
    }
}
