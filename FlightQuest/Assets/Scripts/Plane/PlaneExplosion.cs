using UnityEngine;
using Zenject;

public class PlaneExplosion : MonoBehaviour
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
        }
    }

    private void SetMaxSpeed() => plane.maxSpeed = plane.lowMaxSpeed;

    private void ExchangeBody()
    {
        planeBodies[1].SetActive(!plane.isLandingGearRemoved);
        planeBodies[0].SetActive(plane.isLandingGearRemoved);
    }

    private void DisableButtons()
    {
        foreach (GameObject button in buttons) 
            Destroy(button);
    }
}
