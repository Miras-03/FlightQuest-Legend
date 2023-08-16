using System.Collections;
using System.Diagnostics.SymbolStore;
using TMPro;
using UnityEngine;
using Zenject;

namespace PlaneSection
{
    public sealed class PlaneController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI speedIndicator;

        private Plane plane;
        private PlaneControl planeControl;
        private const float lowSpeed = 100;

        [Inject]
        public void Contruct(Plane plane)
        {
            this.plane = plane;
            planeControl = GetComponent<PlaneControl>();
        }

        private void Start() => StartCoroutine(UpdateEverySeconds());

        private void FixedUpdate()
        {
            plane.Move();
            if (!plane.isBurned)
                planeControl.Control();
        }

        private IEnumerator UpdateEverySeconds()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                UpdateStateBasedOnSpeed();
                speedIndicator.text = $"Speed\n   {(int)plane.currentSpeed}";
            }
        }

        private void UpdateStateBasedOnSpeed()
        {
            if (plane.currentSpeed < lowSpeed)
            {
                planeControl.pitchAmount = PlaneData.pitchGroundAmount;
                planeControl.rollAmount = 0;
            }
            else
            {
                planeControl.pitchAmount = PlaneData.pitchAmount;
                planeControl.rollAmount = PlaneData.rollAmount;
            }
        }
    }
}