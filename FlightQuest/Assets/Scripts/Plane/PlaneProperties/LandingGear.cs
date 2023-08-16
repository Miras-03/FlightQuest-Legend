using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class LandingGear : MonoBehaviour
{
    private Plane plane;
    private Animator anim;

    [SerializeField] private Slider lever;

    private float forceSpeed = 100f;

    [Inject]
    public void Contruct(Plane plane)
    {
        this.plane = plane;
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    public void SetLandingGear()
    {
        plane.isLandingGearRemoved = !plane.isLandingGearRemoved;
        if (!plane.isLandingGearRemoved)
        {
            anim.Play("PullUp");
            plane.maxSpeed += forceSpeed;
            SetLeverValue();
        }
        else
        {
            anim.Play("PullDown");
            plane.maxSpeed -= forceSpeed;
            SetLeverValue();
        }
    }

    private void SetLeverValue()
    {
        float maxSpeed = plane.maxSpeed;
        lever.maxValue = maxSpeed;
        lever.value = maxSpeed;
    }
}
