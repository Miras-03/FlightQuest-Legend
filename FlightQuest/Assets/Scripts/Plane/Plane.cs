using Unity.VisualScripting;
using UnityEngine;

namespace PlaneOption
{
    public class Plane : MonoBehaviour
    {
        [SerializeField] private FloatingJoystick floatingJoystick;

        private Rigidbody rb;

        private float horizontalInput;
        private float verticalInput;

        private float yaw;
        private float pitch;
        private float roll;

        public float maxSpeed = 300f;
        public float currentSpeed = 0f;
        
        public float currentAcceleration;
        private float lowAcceleration = 5f;
        private float mediumAcceleration = 20f;
        private float highAcceleration = 50f;
        private int accelerationCount = 0;

        private const float smoothSpeed = PlaneData.smoothSpeed;

        private const float yawAmount = PlaneData.yawAmount;
        private const float pitchAmount = PlaneData.pitchAmount;
        private const float rollAmount = PlaneData.rollAmount;

        private float baseDrag = 0.01f;
        private float baseLift = 0.01f;

        private float currentYawVelocity;
        private float currentPitchVelocity;
        private float currentRollVelocity;

        private bool isLandingGearRemoved = true;

        private void Awake() => rb = GetComponent<Rigidbody>();

        private void Start()
        {
            SetAccelerationLevel();
        }

        private void FixedUpdate()
        {
            Move();
            Control();
        }

        private void Move()
        {
            float adjustedDrag = baseDrag + rb.velocity.magnitude * baseDrag;
            float adjustedLift = baseLift + rb.velocity.magnitude * baseDrag;

            Vector3 dragForce = -rb.velocity.normalized * adjustedDrag;
            Vector3 liftForce = transform.up * adjustedLift;

            rb.AddForce(dragForce + liftForce);

            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, currentAcceleration * Time.fixedDeltaTime);

            Vector3 movement = transform.forward * currentSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }

        public void Control()
        {
            InputOfJoystick();
            ControlSetting();
        }

        private void InputOfJoystick()
        {
            horizontalInput = floatingJoystick.Horizontal;
            verticalInput = floatingJoystick.Vertical;
        }

        private void ControlSetting()
        {
            yaw = Mathf.SmoothDamp(yaw, horizontalInput * yawAmount, ref currentYawVelocity, smoothSpeed);
            pitch = Mathf.SmoothDamp(pitch, verticalInput * pitchAmount, ref currentPitchVelocity, smoothSpeed);
            roll = Mathf.SmoothDamp(roll, -horizontalInput * rollAmount, ref currentRollVelocity, smoothSpeed);

            transform.localRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);
        }

        public void SetAccelerationLevel()
        {
            if (accelerationCount < 2)
                accelerationCount++;
            else
                accelerationCount = 0;

            switch (accelerationCount)
            {
                case 1:
                    currentAcceleration = lowAcceleration;
                    maxSpeed = 50;
                    break;
                case 2:
                    currentAcceleration = mediumAcceleration;
                    maxSpeed = 150;
                    break;
                default:
                    currentAcceleration = highAcceleration;
                    maxSpeed = 300;
                    break;
            }
        }

        public void SetLandingGear()
        {
            isLandingGearRemoved = !isLandingGearRemoved;
            float currentMaxSpeed = maxSpeed;
            if (!isLandingGearRemoved)
                maxSpeed += 50;
            else
                maxSpeed -= 50;
        }
    }
}