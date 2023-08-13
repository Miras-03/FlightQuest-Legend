using UnityEngine;
using Zenject;

namespace PlaneSection
{
    public class GroundState : MonoBehaviour, IPlaneState
    {
        [SerializeField] private FloatingJoystick joystick;

        private Plane plane;

        private float horizontalInput;
        private const float smoothLandSpeed = 0.1f;

        [Inject]
        public void Construct(Plane plane) => this.plane = plane;

        public void Control()
        {
            InputOfJoystick();
            Vector3 rotation = new Vector3(0f, horizontalInput * smoothLandSpeed, 0f);
            plane.rb.MoveRotation(plane.rb.rotation * Quaternion.Euler(rotation));
        }

        private void InputOfJoystick() => horizontalInput = joystick.Horizontal;
    }
}