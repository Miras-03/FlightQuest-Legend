using UnityEngine;

namespace PlaneSection
{
    public class LightPlane : Plane
    {
        private void Awake() => rb = GetComponent<Rigidbody>();

        public override void Move()
        {
            if (currentSpeed > maxSpeed)
                currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, decelerationFactor * Time.deltaTime);

            currentSpeed = Mathf.MoveTowards(currentSpeed, maxSpeed, speedAcceleration * Time.deltaTime);

            Vector3 movement = transform.forward * currentSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
}