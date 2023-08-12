using System.Collections;
using UnityEngine;
using Zenject;

namespace PlaneSection
{
    public sealed class SpeedManager : MonoBehaviour
    {
        private Plane plane;
        private PlaneLevelAcceleration accelerationLevel;

        private bool isReached = false;

        [Inject]
        public void Contruct(Plane plane, PlaneLevelAcceleration accelerationLevel)
        {
            this.plane = plane;
            this.accelerationLevel = accelerationLevel;
        }

        private void Start()
        {
            plane.accelerationCount = -1;
            StartCoroutine(CallSetAcceleration());
        }

        public void SetAcceleration()
        {
            if (!isReached)
            {
                plane.accelerationCount = (plane.accelerationCount + 1) % 3;
                if (plane.accelerationCount == 2)
                    isReached = !isReached;
            }
            else
            {
                plane.accelerationCount = (plane.accelerationCount + 2) % 3;
                if (plane.accelerationCount == 0)
                    isReached = !isReached;
            }

            plane.currentAcceleration = plane.accelerationCount == 0 ? plane.lowAcceleration : (plane.accelerationCount == 1 ? plane.mediumAcceleration : plane.highAcceleration);
            plane.maxSpeed = plane.accelerationCount == 0 ? plane.lowMaxSpeed : (plane.accelerationCount == 1 ? plane.mediumMaxSpeed : plane.highMaxSpeed);

            accelerationLevel.AccelerationLevel = Mathf.Sqrt(plane.currentAcceleration);
        }

        private IEnumerator CallSetAcceleration()
        {
            yield return new WaitForSeconds(1f);
            SetAcceleration();
        }
    }
}