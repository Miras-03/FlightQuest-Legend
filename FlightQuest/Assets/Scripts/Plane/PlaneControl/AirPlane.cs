using UnityEngine;

namespace PlaneSection
{
    public class AirPlane : MonoBehaviour
    {
        [HideInInspector] public float maxPossibleSpeed;

        public float maxSpeed;
        public float currentSpeed = 0f;

        [HideInInspector] public int speedAcceleration;
        [HideInInspector] public int decelerationFactor;

        [HideInInspector] public bool isLandingGearRemoved = true;
        [HideInInspector] public bool isBurned = false;

        [HideInInspector] public Rigidbody rb;

        private void Awake() => rb = GetComponent<Rigidbody>();

        public void Move()
        {
            if (currentSpeed > maxSpeed)
                currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, decelerationFactor * Time.deltaTime);

            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, speedAcceleration * Time.deltaTime);

            Vector3 movement = transform.forward * currentSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
}