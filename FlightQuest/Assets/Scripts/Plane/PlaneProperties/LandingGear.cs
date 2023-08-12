using Zenject;
using UnityEngine;

public class LandingGear : MonoBehaviour
{
    private Plane plane;
    private Animator anim;

    [Inject]
    public void Contruct(Plane plane)
    {
        this.plane = plane;
        anim = gameObject.GetComponentInChildren<Animator>();
    }

    public void SetLandingGear()
    {
        plane.isLandingGearRemoved = !plane.isLandingGearRemoved;

        float landingGearWeight = 100f;
        if (!plane.isLandingGearRemoved)
        {
            anim.Play("PullUp");
            plane.maxSpeed += landingGearWeight;
        }
        else
        {
            anim.Play("PullDown");
            plane.maxSpeed -= landingGearWeight;
        }
    }
}
