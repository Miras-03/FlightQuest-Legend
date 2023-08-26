using System.Collections;
using UnityEngine;
using Zenject;

namespace PlaneSection
{
    public sealed class PlaneController : MonoBehaviour
    {
        private AirPlane plane;
        private PlaneControl planeControl;

        [HideInInspector] public int pitchAmount;
        [HideInInspector] public int groundPitchAmount;
        [HideInInspector] public int rollAmount;

        private const float lowSpeed = 200;

        private void Awake()
        {
            plane = GetComponent<AirPlane>();
            planeControl = GetComponent<PlaneControl>();
        }

        private void Start() => StartCoroutine(UpdateEverySeconds());

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