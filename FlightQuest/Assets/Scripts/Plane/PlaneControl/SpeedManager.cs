using CameraOption;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlaneSection
{
    public sealed class SpeedManager : MonoBehaviour, IDieable, IFinishable
    {
        public static event Action OnShiftUIElements;

        private Slider speedLever;

        private AirPlane plane;
        private PropellerRotate propellerRotate;
        private PointDetector pointDetector;

        private CameraManager cameraManager;
        private CameraStartPosition cameraStartPosition;

        private UIAnimationManager managerOfUIAnimation;

        private bool isCameraEnabled = false;

        [Inject]
        public void Contruct(CameraManager cameraManager, Slider speedLever, PropellerRotate propellerRotate, 
            UIAnimationManager managerOfUIAnimation, CameraStartPosition cameraStartPosition, AirPlane plane)
        {
            this.plane = plane;
            this.propellerRotate = propellerRotate;
            this.cameraStartPosition = cameraStartPosition;
            this.cameraManager = cameraManager;
            this.speedLever = speedLever;
            this.managerOfUIAnimation = managerOfUIAnimation;

            this.managerOfUIAnimation.OnUIElementsTurnedOn += TurnPointDetectorOn;

            pointDetector = GetComponent<PointDetector>();

            this.speedLever.onValueChanged.AddListener(SetAcceleration);
        }

        private void Start()
        {
            speedLever.minValue = 0;
            speedLever.maxValue = plane.maxPossibleSpeed;
        }

        public void SetAcceleration(float newSpeed)
        {
            if (!isCameraEnabled)
            {
                SetCamera();
                StartCoroutine(WaitForDelay());
            }

            plane.maxSpeed = newSpeed;

            propellerRotate.GetAccelerationLevel(newSpeed);
        }

        private void TurnPointDetectorOn() => pointDetector.enabled = true;

        private void SetCamera()
        {
            isCameraEnabled = !isCameraEnabled;
            cameraStartPosition.enabled = !isCameraEnabled;
            cameraManager.enabled = isCameraEnabled;
        }

        private void SlowDown(float theSpeed)
        {
            plane.maxSpeed = theSpeed;

            plane.rb.drag = 0;
            plane.rb.angularDrag = 0;
        }

        public void ExecuteExplode() => SlowDown(10);
        public void ExecuteFinish() => SlowDown(0);

        private IEnumerator WaitForDelay()
        {
            yield return new WaitForSeconds(1f);
            OnShiftUIElements?.Invoke();
        }
    }
}