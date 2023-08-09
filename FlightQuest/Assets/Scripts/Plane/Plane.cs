using System;
using System.Security.Authentication;
using UnityEngine;

namespace PlaneOption
{
    public class Plane : MonoBehaviour
    {
        [SerializeField] private FloatingJoystick floatingJoystick;
        [SerializeField] private BoxCollider planeCollider;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private GameObject[] planeBodies;

        private Animator anim;

        private Rigidbody rb;

        private float horizontalInput;
        private float verticalInput;

        private float yaw;
        private float pitch;
        private float roll;

        private float maxSpeed;
        private float lowMaxSpeed = 30f;
        private float mediumMaxSpeed = 150f;
        private float highMaxSpeed = 200f;

        public float currentSpeed = 0f;

        private float currentAcceleration;
        private float lowAcceleration = 5f;
        private float mediumAcceleration = 20f;
        private float highAcceleration = 50f;
        public int accelerationCount = -1;

        private const float smoothAirSpeed = PlaneData.smoothAirSpeed;
        private const float smoothLandSpeed = PlaneData.smoothLandSpeed;

        private const float yawAmount = PlaneData.yawAmount;
        private const float pitchAmount = PlaneData.pitchAmount;
        private const float rollAmount = PlaneData.rollAmount;

        private float currentYawVelocity;
        private float currentPitchVelocity;
        private float currentRollVelocity;

        private bool isLandingGearRemoved = true;
        private bool disableJoystickInput = false;
        private bool isReachedMax = false;

        private void Awake()
        {
            anim = gameObject.GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody>();
        }

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
            if (currentSpeed > maxSpeed)
            {
                float decelerationFactor = 30f;
                currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, decelerationFactor * Time.fixedDeltaTime);
            }

            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, currentAcceleration * Time.fixedDeltaTime);

            Vector3 movement = transform.forward * currentSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }

        public void Control()
        {
            if (!disableJoystickInput)
            {
                InputOfJoystick();
                ControlSetting();
            }
        }

        private void InputOfJoystick()
        {
            horizontalInput = floatingJoystick.Horizontal;
            if (currentSpeed > lowMaxSpeed)
                verticalInput = floatingJoystick.Vertical;
        }

        private bool IsTouchingGround()
        {
            Vector3 halfExtents = planeCollider.bounds.extents;
            halfExtents.y += 0.5f;
            Vector3 center = planeCollider.bounds.center;
            center.y -= halfExtents.y;

            bool isTouchingGround = Physics.OverlapBox(center, halfExtents, Quaternion.identity, groundLayerMask).Length > 0;

            return isTouchingGround;
        }

        private void ControlSetting()
        {
            if (currentSpeed <= lowMaxSpeed)
            {
                Vector3 carRotation = new Vector3(0f, horizontalInput * smoothLandSpeed, 0f);
                rb.MoveRotation(rb.rotation * Quaternion.Euler(carRotation));
            }
            else
            {
                yaw = Mathf.SmoothDamp(yaw, horizontalInput * yawAmount, ref currentYawVelocity, smoothAirSpeed);
                pitch = Mathf.SmoothDamp(pitch, verticalInput * pitchAmount, ref currentPitchVelocity, smoothAirSpeed);
                roll = Mathf.SmoothDamp(roll, -horizontalInput * rollAmount, ref currentRollVelocity, smoothAirSpeed);

                transform.localRotation = Quaternion.Euler(Vector3.up * yaw + Vector3.right * pitch + Vector3.forward * roll);
            }
        }

        public void SetAccelerationLevel()
        {
            if (!isReachedMax)
                accelerationCount = (accelerationCount + 1) % 3;
            else
                accelerationCount = (accelerationCount + 2) % 3;

            currentAcceleration = accelerationCount == 0 ? lowAcceleration : (accelerationCount == 1 ? mediumAcceleration : highAcceleration);
            maxSpeed = accelerationCount == 0 ? lowMaxSpeed : (accelerationCount == 1 ? mediumMaxSpeed : highMaxSpeed);
        }

        public void SetLandingGear()
        {
            isLandingGearRemoved = !isLandingGearRemoved;

            float landingGearWeight = 100f;
            if (!isLandingGearRemoved)
            {
                anim.Play("PullUp");
                maxSpeed += landingGearWeight;
            }
            else
            {
                anim.Play("PullDown");
                maxSpeed -= landingGearWeight;
            }
        }

        private void OnCollisionEnter()
        {
            if (!isLandingGearRemoved)
            {
                ExchangeBody();
                maxSpeed = lowMaxSpeed;
                disableJoystickInput = true;
            }
        }

        private void ExchangeBody()
        {
            planeBodies[1].SetActive(!isLandingGearRemoved);
            planeBodies[0].SetActive(isLandingGearRemoved);
        }
    }
}