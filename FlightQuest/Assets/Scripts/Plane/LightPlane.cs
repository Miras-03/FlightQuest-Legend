using UnityEngine;

namespace PlaneSection
{
    public class LightPlane : Plane
    {
        [SerializeField] private BoxCollider planeCollider;
        [SerializeField] private LayerMask groundLayerMask;
        [SerializeField] private GameObject[] planeBodies;

        private void Awake() => rb = GetComponent<Rigidbody>();

        private void Start()
        {
            lowMaxSpeed = 30f;
            mediumMaxSpeed = 100f;
            highMaxSpeed = 200f;

            lowAcceleration = 5;
            mediumAcceleration = 20f;
            highAcceleration = 50f;
        }

        public override void Move()
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
    }
}