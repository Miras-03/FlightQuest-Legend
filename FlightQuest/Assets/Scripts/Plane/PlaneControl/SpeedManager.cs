using CameraOption;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using System;

namespace PlaneSection
{
    public sealed class SpeedManager : MonoBehaviour, IDieable, IFinishable
    {
        private Slider speedLever;
        private UIManager managerOfUI;

        private AirPlane plane;
        private PropellerRotate propellerRotate;
        private PointDetector pointDetector;
        private CameraManager cameraManager;

        private bool isCameraEnabled = false;

        [Inject]
        public void Contruct(UIManager managerOfUI, CameraManager cameraManager, Slider speedLever, PropellerRotate propellerRotate)
        {
            this.propellerRotate = propellerRotate;
            this.managerOfUI = managerOfUI;

            this.cameraManager = cameraManager;
            this.speedLever = speedLever;

            plane = GetComponent<AirPlane>();
            pointDetector = GetComponent<PointDetector>();

            this.speedLever.onValueChanged.AddListener(SetAcceleration);
        }

        private void Start()
        {
            plane.maxPossibleSpeed = 250;
            plane.speedAcceleration = 30;
            plane.decelerationFactor = 30;

            speedLever.minValue = 0;
            speedLever.maxValue = plane.maxPossibleSpeed;
        }

        public void SetAcceleration(float newSpeed)
        {
            if (!isCameraEnabled)
            {
                SetUIObjects();
                SetCamera();
                TurnPointDetectorOn();
            }

            plane.maxSpeed = newSpeed;

            propellerRotate.GetAccelerationLevel(newSpeed);
        }

        private void TurnPointDetectorOn() => pointDetector.enabled = true;

        private void SetCamera()
        {
            isCameraEnabled = !isCameraEnabled;
            cameraManager.enabled = isCameraEnabled;
        }

        private void SetUIObjects() => managerOfUI.SetUIObjects();

        private void SlowDown(float theSpeed)
        {
            plane.maxSpeed = theSpeed;
        }

        public void ExecuteExplode() => SlowDown(10);

        public void ExecuteFinish() => SlowDown(0);
    }
}