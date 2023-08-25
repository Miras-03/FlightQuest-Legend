using UnityEngine;
using System.Collections;

namespace PlaneSection
{
    public sealed class PlaneExplosion : MonoBehaviour, IDieable
    {
        private AirPlane plane;
        [SerializeField] private GameObject[] planeBodies;
        [SerializeField] private ParticleSystem explosionEffect;

        private const int lowSpeed = 10;

        private void Awake() => plane = GetComponent<AirPlane>();

        public void ExecuteExplode()
        {
            Explode();
            StartCoroutine(RestartAfterDelay());
        }

        private void Explode()
        {
            plane.isBurned = true;
            explosionEffect.Play();
            SwapBodies();
            plane.maxSpeed = lowSpeed;
        }

        private void SwapBodies()
        {
            planeBodies[1].SetActive(true);
            planeBodies[0].SetActive(false);
        }

        private IEnumerator RestartAfterDelay()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.RestartScene();
        }
    }
}