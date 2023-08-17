using CameraOption;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlaneSection
{
    public sealed class SpeedManager : MonoBehaviour, IDieable, IFinishable
    {
        [SerializeField] private Slider speedLever;
        [SerializeField] private GameObject[] objectsOfUI;

        private Plane plane;
        private PlaneLevelAcceleration accelerationLevel;
        private PointDetector pointDetector;
        [SerializeField] private CameraManager cameraManager;

        private bool isCameraEnabled = false;

        [Inject]
        public void Contruct(Plane plane, PlaneLevelAcceleration accelerationLevel)
        {
            this.plane = plane;
            this.accelerationLevel = accelerationLevel;

            pointDetector = GetComponent<PointDetector>();

            speedLever.onValueChanged.AddListener(SetAcceleration);
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

            accelerationLevel.AccelerationLevel = Mathf.Sqrt(newSpeed);
        }

        private void TurnPointDetectorOn() => pointDetector.enabled = true;

        private void SetCamera()
        {
            isCameraEnabled = !isCameraEnabled;
            cameraManager.enabled = isCameraEnabled;
        }

        private void SetUIObjects()
        {
            objectsOfUI[0].SetActive(false);
            objectsOfUI[1].SetActive(true);
        }

        private void SlowDown(float theSpeed)
        {
            accelerationLevel.AccelerationLevel = 5;
            plane.maxSpeed = theSpeed;
        }

        public void ExecuteExplode() => SlowDown(10);

        public void ExecuteFinish() => SlowDown(0);
    }
}