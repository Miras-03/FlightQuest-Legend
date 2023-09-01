using CameraOption;
using PlaneSection;
using System.Collections;
using UnityEngine;
using Zenject;

public sealed class LandingGearManager : MonoBehaviour
{
    private Landing landing;
    private CameraManager cameraManager;
    private LandingGear landingGear;
    private PointDetector pointDetector;

    private bool entered = false;

    private bool injected = false;

    [Inject]
    public void Initialize(PrefabInitializationNotifier notifier, 
        Landing landing, CameraManager cameraManager)
    {
        this.landing = landing; 
        this.cameraManager = cameraManager;

        notifier.OnPrefabInitialized += InjectAfterDelay;
    }

    private void InjectAfterDelay()
    {
        if (!injected)
        {
            injected = true;

            landingGear = FindObjectOfType<LandingGear>();
            pointDetector = FindObjectOfType<PointDetector>();

            landing.AddObservers(landingGear);
            landing.AddObservers(pointDetector);
            landing.AddObservers(cameraManager);
        }
    }

    private void OnDisable() => landing.RemoveObservers();

    private void OnTriggerEnter()
    {
        if (!entered)
        {
            entered = !entered;
            StartCoroutine(WaitForDelay());

            landing.NotifyObserversAboutLand();
        }
    }

    private IEnumerator WaitForDelay()
    {
        yield return new WaitForSeconds(3);
        entered = !entered;
    }
}
