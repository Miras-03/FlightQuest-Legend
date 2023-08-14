using UnityEngine;
using Zenject;

namespace PlaneSection
{
    public sealed class ControlManager : MonoBehaviour, IDieable
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
            groundState = GetComponent<GroundState>();
            flyingState = GetComponent<FlyingState>();
        }

        private void Update()
        {
            currentPlane.Move();
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

        public void ExecuteExplode() => Destroy(this);
    }
}