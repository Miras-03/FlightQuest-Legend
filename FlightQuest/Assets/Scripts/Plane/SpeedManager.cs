using System.Collections;
using UnityEngine;
using Zenject;

namespace PlaneSection
{
    public sealed class SpeedManager : MonoBehaviour
    {
        private Plane plane;

        [Inject]
        public void Contruct(Plane plane) => this.plane = plane;

        private void Start()
        {
            plane.accelerationCount = -1;
            StartCoroutine(CallSetAcceleration());
        }

        public void SetAcceleration()
        {
            plane.accelerationCount = (plane.accelerationCount + 1) % 3;
            plane.currentAcceleration = plane.accelerationCount == 0 ? plane.lowAcceleration : (plane.accelerationCount == 1 ? plane.mediumAcceleration : plane.highAcceleration);
            plane.maxSpeed = plane.accelerationCount == 0 ? plane.lowMaxSpeed : (plane.accelerationCount == 1 ? plane.mediumMaxSpeed : plane.highMaxSpeed);
        }

        private IEnumerator CallSetAcceleration()
        {
            yield return new WaitForSeconds(1f);
            SetAcceleration();
        }
    }
}