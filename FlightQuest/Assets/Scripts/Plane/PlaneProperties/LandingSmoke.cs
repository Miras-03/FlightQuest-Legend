using UnityEngine;

public class LandingSmoke : MonoBehaviour
{
    [SerializeField] private ParticleSystem smoke;

    private void OnCollisionEnter(Collision collision)
    {
        float impactForceThreshold = 10f;
        Vector3 relativeVelocity = collision.relativeVelocity;
        float impactForce = relativeVelocity.magnitude;

        if(impactForce > impactForceThreshold)
            smoke.Play();
    }
}
