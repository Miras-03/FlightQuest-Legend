using TMPro;
using UnityEngine;
using Zenject;

namespace PlaneSection
{
    public sealed class ControlManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI speedIndicator;

        private Plane plane;

        private IPlaneState currentPlaneState;

        private GroundState groundState;
        private FlyingState flyingState;

        [Inject]
        public void Contruct(IPlaneState planeState, Plane plane)
        {
            currentPlaneState = planeState;
            this.plane = plane;

            groundState = GetComponent<GroundState>();
            flyingState = GetComponent<FlyingState>();
        }

        private void Update()
        {
            plane.Move();
            if (!plane.isBurned)
                currentPlaneState.Control();

            UpdateStateBasedOnSpeed();

            speedIndicator.text = $"Speed\n   {(int)plane.currentSpeed}";
        }

        private void UpdateStateBasedOnSpeed()
        {
            if (plane.currentSpeed > plane.lowMaxSpeed)
                ChangeState(flyingState);
            else
                ChangeState(groundState);
        }

        private void ChangeState(IPlaneState newState) => currentPlaneState = newState;
    }
}