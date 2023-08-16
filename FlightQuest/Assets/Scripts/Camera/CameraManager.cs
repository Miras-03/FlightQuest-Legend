using System.Collections;
using UnityEngine;

namespace CameraOption
{
    public sealed class CameraManager : MonoBehaviour
    {
        [SerializeField] private Transform target;

        public Vector3 locationOffset = new Vector3(238.6f, 100f, 0);
        public Vector3 rotationOffset = new Vector3(14f, -89.988f, 0);

        [HideInInspector] private float upCoordOfCamera = 100f;
        [HideInInspector] private float upRotationOfCamera = 14f;
        [HideInInspector] private float downCoordOfCamera = 50f;
        [HideInInspector] private float downRotationOfCamera = 3.3f;

        private const float smoothSpeed = 0.125f;

        private bool isUpCoord = false;

        private void FixedUpdate()
        {
            Vector3 desiredPosition = target.position + target.rotation * locationOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset);
            Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
            transform.rotation = smoothedrotation;
        }

        public void ChangeCameraCoord()
        {
            isUpCoord = !isUpCoord;

            Vector3 targetLocationOffset;
            Vector3 targetRotationOffset;

            if (!isUpCoord)
            {
                targetLocationOffset = new Vector3(locationOffset.x, upCoordOfCamera, locationOffset.z);
                targetRotationOffset = new Vector3(upRotationOfCamera, rotationOffset.y, rotationOffset.z);
            }
            else
            {
                targetLocationOffset = new Vector3(locationOffset.x, downCoordOfCamera, locationOffset.z);
                targetRotationOffset = new Vector3(downRotationOfCamera, rotationOffset.y, rotationOffset.z);
            }

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