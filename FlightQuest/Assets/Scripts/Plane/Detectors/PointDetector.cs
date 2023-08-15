using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlaneSection
{
    public sealed class PointDetector : MonoBehaviour
    {
        [SerializeField] private Button lever;
        [SerializeField] private ParticleSystem selectionEffect;

        private Plane plane;
        private PlaneLevelAcceleration accelerationLevel;

        [SerializeField] private int petrolLitres;
        private bool isReachedThePoint = false;

        private const int startingPetrolLitres = 100;
        private const int perLiter = 5;
        private const int lowPetrolLevel = 0;
        private const int restartDelaySeconds = 5;

        [Inject]
        public void Construct(Plane plane, PlaneLevelAcceleration accelerationLevel)
        {
            this.plane = plane;
            this.accelerationLevel = accelerationLevel;
        }

        private void Start()
        {
            petrolLitres = startingPetrolLitres;
            StartCoroutine(OffLitresTimer());
        }

        public IEnumerator OffLitresTimer()
        {
            while (petrolLitres > lowPetrolLevel)
            {
                yield return new WaitForSeconds(1);
                petrolLitres -= perLiter;
            }

            SetDefaultSpeed();
            lever.interactable = false;
            StartCoroutine(TimerForRestartGame());
        }

        private IEnumerator TimerForRestartGame()
        {
            isReachedThePoint = false;
            yield return new WaitForSeconds(restartDelaySeconds);
            if (!isReachedThePoint)
                SceneManager.RestartScene();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Point"))
            {
                isReachedThePoint = true;
                selectionEffect.Play();
                petrolLitres = startingPetrolLitres;
                lever.interactable = true;
                SetSpeed();
                Destroy(other.gameObject);
                StartCoroutine(OffLitresTimer());
            }
        }

        private void SetSpeed() => plane.maxSpeed = !plane.isLandingGearRemoved ? plane.maxPossibleSpeed : plane.highMaxSpeed;

        private void SetDefaultSpeed()
        {
            plane.maxSpeed = plane.lowMaxSpeed;
            accelerationLevel.AccelerationLevel = Mathf.Sqrt(plane.lowAcceleration);
        }

        public void BreakDown() => Destroy(this);
    }
}
