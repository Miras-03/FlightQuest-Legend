using System.Collections;
using UnityEngine;

namespace CameraOption
{
    public sealed class CameraManager : MonoBehaviour, ILandable
    {
        private PlaneData planeData;
        private Transform target;

        [SerializeField] private Vector3 locationOffset = new Vector3(0f, 80f, -200);
        [SerializeField] private Vector3 rotationOffset = new Vector3(10f, 0f, 0);

        private float upCoordOfCamera = 80f;
        private float upRotationOfCamera = 10f;
        private float downCoordOfCamera = 30f;
        private float downRotationOfCamera = 0f;

        private float heavyUpCoordOfCamera = 100f;
        private float heavyDownCoordOfCamera = 40f;

        private float heavyZCoord = -320;

        private const float smoothSpeed = 0.125f;
        private bool hasEntered = false;

        public void OnPlaneInstanceReady(GameObject planeInstance, PlaneData planeData)
        {
            target = planeInstance.transform;
            this.planeData = planeData;

            if (planeData.uniqueCode != 0)
            {
                upCoordOfCamera = heavyUpCoordOfCamera;
                downCoordOfCamera = heavyDownCoordOfCamera;

                locationOffset = new Vector3(0f, heavyUpCoordOfCamera, heavyZCoord);
            }
        }

        private void FixedUpdate()
        {
            Vector3 desiredPosition = target.position + target.rotation * locationOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset);
            Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
            transform.rotation = smoothedrotation;
        }

        public void ExecuteLand()
        {
            Vector3 targetLocationOffset;
            Vector3 targetRotationOffset;

            if (hasEntered)
            {
                targetLocationOffset = new Vector3(locationOffset.x, upCoordOfCamera, locationOffset.z);
                targetRotationOffset = new Vector3(upRotationOfCamera, rotationOffset.y, rotationOffset.z);
            }
            else
            {
                targetLocationOffset = new Vector3(locationOffset.x, downCoordOfCamera, locationOffset.z);
                targetRotationOffset = new Vector3(downRotationOfCamera, rotationOffset.y, rotationOffset.z);
            }

            hasEntered = !hasEntered;
            float transitionDuration = 1.0f;

            StartCoroutine(SmoothlyTransitionCamera(targetLocationOffset, targetRotationOffset, transitionDuration));
        }

        private IEnumerator SmoothlyTransitionCamera(Vector3 targetLocationOffset, Vector3 targetRotationOffset, float duration)
        {
            Vector3 initialLocationOffset = locationOffset;
            Vector3 initialRotationOffset = rotationOffset;

            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                locationOffset = Vector3.Lerp(initialLocationOffset, targetLocationOffset, elapsedTime / duration);
                rotationOffset = Vector3.Lerp(initialRotationOffset, targetRotationOffset, elapsedTime / duration);

                yield return null;
                elapsedTime += Time.deltaTime;
            }

            locationOffset = targetLocationOffset;
            rotationOffset = targetRotationOffset;
        }
    }
}