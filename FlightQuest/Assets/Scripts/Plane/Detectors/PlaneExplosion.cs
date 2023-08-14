using UnityEngine;
using System.Collections;
using Zenject;

namespace PlaneSection
{
    public sealed class PlaneExplosion : MonoBehaviour, IDieable
    {
        private Plane plane;
        [SerializeField] private GameObject[] planeBodies;
        [SerializeField] private GameObject[] buttons;

        [Inject]
        public void Construct(Plane plane) => this.plane = plane;

        public void ExecuteExplode()
        {
            Explode();
            DestroyButtons();
            StartCoroutine(RestartAfterDelay());
        }

        private void Explode()
        {
            plane.isBurned = true;
            planeBodies[1].SetActive(true);
            planeBodies[0].SetActive(false);
            plane.maxSpeed = plane.lowMaxSpeed;
        }

        private void DestroyButtons()
        {
            foreach (GameObject button in buttons)
                Destroy(button);
        }

        private IEnumerator RestartAfterDelay()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.RestartScene();
        }
    }
}