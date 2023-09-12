using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace PlaneSection
{
    public sealed class PointDetector : MonoBehaviour, ILandable
    {
        private SceneManager sceneManager;
        private PetrolLevel petrolLevel;
        private AirPlane plane;
        private PropellerRotate propellerRotate;

        private Slider lever;
        private AudioSource selectionSound;

        private Coroutine petrolCoroutine;

        private float petrolLitres;

        private const int startingPetrolLitres = 100;
        private const float perLitre = 0.1f;
        private const int lowPetrolLevel = 0;

        private bool isFailed = false;
        private bool hasEntered = false;

        [Inject]
        public void Construct(SceneManager sceneManager, PetrolLevel petrolLevel, Slider lever, 
            PropellerRotate propellerRotate, AudioSource[] selectionSound)
        {
            this.sceneManager = sceneManager;
            this.petrolLevel = petrolLevel;
            this.lever = lever;
            this.propellerRotate = propellerRotate;
            this.selectionSound = selectionSound[9];

            plane = GetComponent<AirPlane>();
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
                yield return new WaitForFixedUpdate();
                DecreasePetrolLevel();
            }

            isFailed = true;
            StartCoroutine(WaitForDelay());
            SetDefaultSpeed();
            lever.onValueChanged.RemoveAllListeners();
        }

        private void DecreasePetrolLevel()
        {
            petrolLitres -= perLitre;
            petrolLevel.SetPetrolLevel(perLitre);
        }

        private IEnumerator WaitForDelay()
        {
            yield return new WaitForSeconds(5);
            sceneManager.RestartScene();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Point") && !isFailed)
            {
                selectionSound.Play();
                SetValue();
                Destroy(other.gameObject);

                if (petrolCoroutine != null)
                    StopCoroutine(petrolCoroutine);
                petrolCoroutine = StartCoroutine(OffLitresTimer());
            }
            else if (other.CompareTag("Finish"))
                ExecuteLand();
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
            propellerRotate.GetAccelerationLevel(0);
        }

        public void ExecuteLand()
        {
            if (hasEntered)
                Destroy(this);
            hasEntered = !hasEntered;
        }
    }
}
