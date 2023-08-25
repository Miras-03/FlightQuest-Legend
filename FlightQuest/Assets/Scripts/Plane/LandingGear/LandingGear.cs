using Zenject;
using UnityEngine;
using UnityEngine.UI;
using PlaneSection;

public sealed class LandingGear : MonoBehaviour, ILandable
{
    private AirPlane plane;
    [SerializeField] private Animator anim;

    private Slider lever;

    private float forceSpeed = 100f;
    private bool hasEntered = false;

    [Inject]
    public void Construct(Slider lever)
    {
        this.lever = lever;

        plane = GetComponent<AirPlane>();
    }

    public void ExecuteLand()
    {
        if (!hasEntered)
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
        hasEntered = !hasEntered;
    }

    private void SetLeverValue()
    {
        float maxSpeed = plane.maxSpeed;
        lever.maxValue = maxSpeed;
        lever.value = maxSpeed;
    }
}
