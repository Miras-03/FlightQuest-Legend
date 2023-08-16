using UnityEngine;
using Zenject;

namespace PlaneSection
{
    public class PlaneControl : MonoBehaviour
    {
        [SerializeField] private FixedJoystick joystick;

        private Plane plane;

        private float yaw;
        private float pitch;
        private float roll;

        private const float yawAmount = PlaneData.yawAmount;
        [HideInInspector] public float pitchAmount = PlaneData.pitchAmount;
        [HideInInspector] public float rollAmount = PlaneData.rollAmount;

        private const float rotationSmoothSpeed = PlaneData.rotationSmoothSpeed;

        private float currentYawVelocity;
        private float currentPitchVelocity;
        private float currentRollVelocity;

        private float horizontalInput;
        private float verticalInput;

        [Inject]
        public void Construct(Plane plane) => this.plane = plane;

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