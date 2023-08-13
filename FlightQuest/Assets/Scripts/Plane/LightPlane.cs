using UnityEngine;

namespace PlaneSection
{
    public class LightPlane : Plane
    {
        [SerializeField] private BoxCollider planeCollider;

        private void Awake() => rb = GetComponent<Rigidbody>();

        private void Start()
        {
            maxPossibleSpeed = 300f;

            lowMaxSpeed = 30f;
            mediumMaxSpeed = 100f;
            highMaxSpeed = 200f;

            lowAcceleration = 5f;
            mediumAcceleration = 20f;
            highAcceleration = 50f;
        }

        public override void Move()
        {
            if (currentSpeed > maxSpeed)
            {
                float decelerationFactor = 30f;
                currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, decelerationFactor * Time.deltaTime);
            }

            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, currentAcceleration * Time.deltaTime);

            Vector3 movement = transform.forward * currentSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
}