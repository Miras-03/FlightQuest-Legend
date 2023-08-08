using UnityEngine;

namespace CameraOption
{
    public sealed class CameraManager : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private Vector3 locationOffset = new Vector3(238.6f, 100f, 0);
        private Vector3 rotationOffset = new Vector3(14f, -89.988f, 0);

        private const float smoothSpeed = 0.125f;

        private void FixedUpdate()
        {
            Vector3 desiredPosition = target.position + target.rotation * locationOffset;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;

            Quaternion desiredrotation = target.rotation * Quaternion.Euler(rotationOffset);
            Quaternion smoothedrotation = Quaternion.Lerp(transform.rotation, desiredrotation, smoothSpeed);
            transform.rotation = smoothedrotation;
        }
    }
}