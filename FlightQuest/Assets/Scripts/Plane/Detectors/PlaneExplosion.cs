using UnityEngine;
using System.Collections;
using Zenject;

namespace PlaneSection
{
    public sealed class PlaneExplosion : MonoBehaviour
    {
        private Plane plane;

        [SerializeField] private GameObject[] planeBodies;
        [SerializeField] private GameObject[] buttons;

        [Inject]
        public void Contruct(Plane plane) => this.plane = plane;

        private void OnCollisionEnter()
        {
            if (!plane.isLandingGearRemoved)
            {
                plane.isBurned = true;
                ExchangeBody();
                SetMaxSpeed();
                DisableButtons();

                StartCoroutine(TimerForRestartGame());
            }
        }

        private void SetMaxSpeed() => plane.maxSpeed = plane.lowMaxSpeed;

        private void ExchangeBody()
        {
            planeBodies[1].SetActive(true);
            planeBodies[0].SetActive(false);
        }

        private void DisableButtons()
        {
            foreach (GameObject button in buttons)
                Destroy(button);
        }

        private IEnumerator TimerForRestartGame()
        {
            yield return new WaitForSeconds(3f);
            SceneManager.RestartScene();
        }
    }
}