using System.Collections;
using UnityEngine;

namespace PlaneSection
{
    public sealed class PlaneController : MonoBehaviour
    {
        private AirPlane plane;
        private PlaneControl planeControl;

        private PlaneData planeData;

        private int pitchAmount;
        private int groundPitchAmount;
        private int rollAmount;

        private const float lowSpeed = 100;

        private void Awake()
        {
            plane = GetComponent<AirPlane>();
            planeControl = GetComponent<PlaneControl>();
        }

        private void Start()
        {
            planeControl.yawAmount = 50;//planeData.yawAmount;
            pitchAmount = 50;//planeData.pitchAmount;
            groundPitchAmount = 50;//planeData.groundPitchAmount; 
            rollAmount = 50;

            StartCoroutine(UpdateEverySeconds());
        }

        private void FixedUpdate()
        {
            plane.Move();
            if (!plane.isBurned)
                planeControl.Control();
        }

        private IEnumerator UpdateEverySeconds()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                UpdateStateBasedOnSpeed();
            }
        }

        private void UpdateStateBasedOnSpeed()
        {
            if (plane.currentSpeed < lowSpeed)
            {
                planeControl.pitchAmount = groundPitchAmount;
                planeControl.rollAmount = 0;
            }
            else
            {
                planeControl.pitchAmount = pitchAmount;
                planeControl.rollAmount = rollAmount;
            }
        }
    }
}