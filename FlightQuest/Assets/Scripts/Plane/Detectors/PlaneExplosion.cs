using UnityEngine;
using System.Collections;
using Zenject;

namespace PlaneSection
{
    public sealed class PlaneExplosion : MonoBehaviour, IDieable
    {
        private SceneManager sceneManager;
        private AirPlane plane;
        [SerializeField] private GameObject[] planeBodies;
        [SerializeField] private ParticleSystem explosionEffect;

        private AudioSource[] explosionSound;

        private const int lowSpeed = 10;

        [Inject]
        public void Construct(SceneManager sceneManager, AudioSource[] explosionSound)
        {
            this.sceneManager = sceneManager;
            this.explosionSound = explosionSound;
        }

        private void Awake() => plane = GetComponent<AirPlane>();

        public void ExecuteExplode()
        {
            Explode();
            StartCoroutine(RestartAfterDelay());
        }

        private void Explode()
        {
            explosionSound[1].Play();
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
            sceneManager.RestartScene();
        }
    }
}