using UnityEngine;
using Zenject;

namespace PlaneSection
{
    public sealed class PlaneControl : MonoBehaviour
    {
        private FixedJoystick joystick;

        private AirPlane plane;

        private float yaw;
        private float pitch;
        private float roll;

        [HideInInspector] public float yawAmount;
        [HideInInspector] public float pitchAmount;
        [HideInInspector] public float rollAmount;

        private float rotationSmoothSpeed;

        private float currentYawVelocity;
        private float currentPitchVelocity;
        private float currentRollVelocity;

        private float horizontalInput;
        private float verticalInput;

        [Inject]
        public void Constuct(FixedJoystick joystick)
        {
            this.joystick = joystick;
        }

        private void Awake()
        {
            plane = GetComponent<AirPlane>();
        }

        public void Control()
        {
            InputOfJoystick();

            yaw = Mathf.SmoothDamp(yaw, horizontalInput * yawAmount, ref currentYawVelocity, rotationSmoothSpeed);
            pitch = Mathf.SmoothDamp(pitch, verticalInput * pitchAmount, ref currentPitchVelocity, rotationSmoothSpeed);
            roll = Mathf.SmoothDamp(roll, -horizontalInput * rollAmount, ref currentRollVelocity, rotationSmoothSpeed);

            plane.rb.rotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);
        }

        private void InputOfJoystick()
        {
            horizontalInput = joystick.Horizontal;
            verticalInput = joystick.Vertical;
        }
    }
}