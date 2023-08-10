using UnityEngine;
using Zenject;

namespace PlaneSection
{
    public sealed class PlaneManager : MonoBehaviour
    {
        private Plane currentPlane;

        private IPlaneState currentPlaneState;

        private GroundState groundState;
        private FlyingState flyingState;

        [Inject]
        public void Contruct(IPlaneState planeState, Plane plane)
        {
            currentPlaneState = planeState;
            currentPlane = plane;
        }

        private void Awake()
        {
            groundState = GetComponent<GroundState>();
            flyingState = GetComponent<FlyingState>();
        }

        private void FixedUpdate()
        {
            currentPlane.Move();
            if(!currentPlane.isBurned)
                currentPlaneState.Control();

            UpdateStateBasedOnSpeed();
        }

        private void UpdateStateBasedOnSpeed()
        {
            if (currentPlane.currentSpeed > currentPlane.lowMaxSpeed)
                ChangeState(flyingState);
            else
                ChangeState(groundState);
        }

        private void ChangeState(IPlaneState newState) => currentPlaneState = newState;
    }
}