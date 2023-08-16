using System;
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

        private Plane plane;
        private PlaneLevelAcceleration accelerationLevel;
        private Rigidbody rb;

        [SerializeField] private int petrolLitres;

        private const int startingPetrolLitres = 100;
        private const int perLiter = 10;
        private const int lowPetrolLevel = 0;

        [Inject]
        public void Construct(Plane plane, PlaneLevelAcceleration accelerationLevel)
        {
            this.plane = plane;
            this.accelerationLevel = accelerationLevel;

            rb = GetComponent<Rigidbody>();
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

            StartCoroutine(WaitForDelay());
            SetDefaultSpeed();
            lever.onValueChanged.RemoveAllListeners();
        }

        private IEnumerator WaitForDelay()
        {
            yield return new WaitForSeconds(5);
            SceneManager.RestartScene();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Point"))
            {
                selectionEffect.Play();
                SetValue();
                Destroy(other.gameObject);
                StartCoroutine(OffLitresTimer());
            }
        }

        private void SetValue()
        {
            lever.interactable = true;
            petrolLitres = startingPetrolLitres;
        }

        private void SetDefaultSpeed()
        {
            plane.maxSpeed = 0;
            accelerationLevel.AccelerationLevel = 0;
            rb.drag = 0;
        }

        public void BreakDown() => Destroy(this);
    }
}
