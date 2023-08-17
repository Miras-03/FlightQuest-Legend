using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlaneSection
{
    public sealed class PointDetector : MonoBehaviour
    {
        [SerializeField] private Slider lever;
        [SerializeField] private ParticleSystem selectionEffect;

        [SerializeField] private ExecuteFinishObservers executeFinishObserver;
        [SerializeField] private PetrolLevel petrolLevel;

        private Plane plane;
        private PlaneLevelAcceleration accelerationLevel;

        private Coroutine petrolCoroutine;

        [SerializeField] private float petrolLitres;

        private const int startingPetrolLitres = 100;
        private const float perLiter = 0.01f;
        private const int lowPetrolLevel = 0;

        private bool isFailed = false;

        [Inject]
        public void Construct(Plane plane, PlaneLevelAcceleration accelerationLevel)
        {
            this.plane = plane;
            this.accelerationLevel = accelerationLevel;
        }

        private void Start()
        {
            SetMaxValueForPetrol();
            StartCoroutine(OffLitresTimer());
        }

        private IEnumerator OffLitresTimer()
        {
            while (petrolLitres > lowPetrolLevel)
            {
                yield return new WaitForEndOfFrame();
                DecreasePetrolLevel();
            }

            isFailed = true;
            StartCoroutine(WaitForDelay());
            SetDefaultSpeed();
            lever.onValueChanged.RemoveAllListeners();
        }

        private void DecreasePetrolLevel()
        {
            petrolLitres -= perLiter;
            petrolLevel.SetPetrolLevel(perLiter);
        }

        private IEnumerator WaitForDelay()
        {
            yield return new WaitForSeconds(5);
            SceneManager.RestartScene();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Point") && !isFailed)
            {
                selectionEffect.Play();
                SetValue();
                Destroy(other.gameObject);

                if (petrolCoroutine != null)
                    StopCoroutine(petrolCoroutine);
                petrolCoroutine = StartCoroutine(OffLitresTimer());
            }
        }

        private void SetMaxValueForPetrol()
        {
            petrolLitres = startingPetrolLitres;
            petrolLevel.SetMaxLevel = startingPetrolLitres;
        }

        private void SetValue()
        {
            petrolLitres = startingPetrolLitres;
            petrolLevel.SetMaxLevel = startingPetrolLitres;
        }

        private void SetDefaultSpeed()
        {
            plane.maxSpeed = 0;
            accelerationLevel.AccelerationLevel = 0;
        }

        public void BreakDown() => Destroy(this);
    }
}
