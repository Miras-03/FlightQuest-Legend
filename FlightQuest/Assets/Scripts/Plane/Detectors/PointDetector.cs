using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlaneSection
{
    public sealed class PointDetector : MonoBehaviour
    {
        [SerializeField] private Button speedButton;

        private Plane plane;

        private int petrolLitres;

        private bool isReachedThePoint = false;

        [Inject]
        public void Construct(Plane plane) => this.plane = plane;

        private void Start()
        {
            petrolLitres = 100;
            StartCoroutine(OffLitresTimer());
        }

        public IEnumerator OffLitresTimer()
        {
            const int lowPetrolLevel = 0;
            const int waitForSeconds = 1;
            const int perLiter = 10;

            while (petrolLitres > lowPetrolLevel)
            {
                yield return new WaitForSeconds(waitForSeconds);
                petrolLitres -= perLiter;
            }

            speedButton.interactable = false;
            plane.maxSpeed = plane.lowMaxSpeed;
            StartCoroutine(TimerForRestartGame());
        }

        private IEnumerator TimerForRestartGame()
        {
            isReachedThePoint = false;
            yield return new WaitForSeconds(3f);
            if (!isReachedThePoint)
                SceneManager.RestartScene();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Point"))
            {
                isReachedThePoint = true;
                petrolLitres = 100;
                speedButton.interactable = true;
                SetSpeed();
                Destroy(other.gameObject);
                StartCoroutine(OffLitresTimer());
            }
        }

        private void SetSpeed()
        {
            if (plane.isLandingGearRemoved)
            {
                plane.maxSpeed = plane.maxPossibleSpeed;
                plane.currentSpeed = plane.maxPossibleSpeed;
            }
            else
            {
                plane.maxSpeed = plane.highMaxSpeed;
                plane.currentSpeed = plane.highMaxSpeed;
            }
        }
    }
}