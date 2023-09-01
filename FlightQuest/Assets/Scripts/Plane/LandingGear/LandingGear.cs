using Zenject;
using UnityEngine;
using UnityEngine.UI;
using PlaneSection;
using System.Collections;

public sealed class LandingGear : MonoBehaviour, ILandable
{
    private SceneManager sceneManager;
    private AirPlane plane;
    [SerializeField] private Animator[] animators;

    private Slider lever;

    private float forceSpeed = 100f;

    private const string PullUp = nameof(PullUp);
    private const string PullDown = nameof(PullDown);

    private bool hasEntered = false;
    private bool hasExited = false;

    [Inject]
    public void Construct(SceneManager sceneManager, Slider lever)
    {
        this.sceneManager = sceneManager;
        this.lever = lever;

        plane = GetComponent<AirPlane>();
    }

    public void ExecuteLand()
    {
        if (!hasEntered)
        {
            foreach (Animator anim in animators)
                anim.Play(PullUp);

            plane.maxPossibleSpeed += forceSpeed;
            SetLeverValue();
        }
        else
        {
            foreach (Animator anim in animators)
                anim.Play(PullDown);

            plane.maxPossibleSpeed -= forceSpeed;
            SetLeverValue();
            StartCoroutine(WaitForLose());
        }
        hasEntered = !hasEntered;
    }

    private void SetLeverValue()
    {
        float maxSpeed = plane.maxSpeed;
        lever.maxValue = maxSpeed;
    }

    private IEnumerator WaitForLose()
    {
        yield return new WaitForSeconds(30);
        sceneManager.RestartScene();
    }
}
