using UnityEngine;
using System.Collections;
using System;

namespace PlaneSection
{
    public sealed class PlaneExplosion : MonoBehaviour, IDieable
    {
        private AirPlane plane;
        [SerializeField] private GameObject[] planeBodies;

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