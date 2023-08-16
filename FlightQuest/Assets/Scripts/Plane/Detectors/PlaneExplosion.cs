using UnityEngine;
using System.Collections;
using Zenject;

namespace PlaneSection
{
    public sealed class PlaneExplosion : MonoBehaviour, IDieable
    {
        private Plane plane;
        [SerializeField] private GameObject[] planeBodies;

        private const int lowSpeed = 10;

        [Inject]
        public void Construct(Plane plane) => this.plane = plane;

        public void ExecuteExplode()
        {
            Explode();
            StartCoroutine(RestartAfterDelay());
        }

        private void Explode()
        {
            plane.isBurned = true;
            planeBodies[1].SetActive(true);
            planeBodies[0].SetActive(false);
            plane.maxSpeed = lowSpeed;
        }

        private IEnumerator RestartAfterDelay()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.RestartScene();
        }
    }
}