using Zenject;
using UnityEngine;
using UnityEngine.UI;
using PlaneSection;

public class LandingGear : MonoBehaviour
{
    private AirPlane plane;
    private Animator anim;

    private Slider lever;

    private float forceSpeed = 100f;

    [Inject]
    public void Construct(Slider lever)
    {
        this.lever = lever;
        plane = GetComponent<AirPlane>();
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
