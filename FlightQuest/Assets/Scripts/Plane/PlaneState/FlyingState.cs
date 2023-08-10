using UnityEngine;

namespace PlaneSection
{
    public class FlyingState : MonoBehaviour, IPlaneState
    {
        [SerializeField] private FloatingJoystick joystick;

        private float yaw;
        private float pitch;
        private float roll;

        private const float yawAmount = PlaneData.yawAmount;
        private const float pitchAmount = PlaneData.pitchAmount;
        private const float rollAmount = PlaneData.rollAmount;

        protected float currentYawVelocity;
        protected float currentPitchVelocity;
        protected float currentRollVelocity;

        private float horizontalInput;
        private float verticalInput;
        private const float smoothAirSpeed = 1.4f;

        public void Control()
        {
            InputOfJoystick();

            yaw = Mathf.SmoothDamp(yaw, horizontalInput * yawAmount, ref currentYawVelocity, smoothAirSpeed);
            pitch = Mathf.SmoothDamp(pitch, verticalInput * pitchAmount, ref currentPitchVelocity, smoothAirSpeed);
            roll = Mathf.SmoothDamp(roll, -horizontalInput * rollAmount, ref currentRollVelocity, smoothAirSpeed);

            transform.localRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);
        }

        private void InputOfJoystick()
        {
            horizontalInput = joystick.Horizontal;
            verticalInput = joystick.Vertical;
        }
    }
}