using UnityEngine;

namespace PlaneSection
{
    public class GroundState : MonoBehaviour, IPlaneState
    {
        [SerializeField] private FloatingJoystick joystick;

        private Rigidbody rb;

        private float horizontalInput;
        private const float smoothLandSpeed = 0.9f;

        private void Awake() => rb = GetComponent<Rigidbody>();

        public void Control()
        {
            InputOfJoystick();
            Vector3 rotation = new Vector3(0f, horizontalInput * smoothLandSpeed, 0f);
            rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        }

        private void InputOfJoystick() => horizontalInput = joystick.Horizontal;
    }
}