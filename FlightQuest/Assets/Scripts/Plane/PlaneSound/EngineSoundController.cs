using PlaneSection;
using System.Collections;
using UnityEngine;
using Zenject;

public sealed class EngineSoundController : MonoBehaviour, IDieable
{
    private AudioSource[] engineSound;
    private AirPlane plane;

    private float pitch;

    private const float minPitch = 0.5f;
    private const float maxPitch = 3f;

    private float currentSpeed;
    private float normalizedSpeed;
    private const float minSpeed = 0f;
    private float maxSpeed;

    private bool injected = false;

    [Inject]
    public void Initialize(PrefabInitializationNotifier notifier, AudioSource[] engineSound)
    {
        this.engineSound = engineSound;
        notifier.OnPrefabInitialized += InjectAfterDelay;
    }

    private void InjectAfterDelay()
    {
        if (!injected)
        {
            injected = true;

            plane = GetComponent<AirPlane>();
            maxSpeed = plane.maxPossibleSpeed;

            StartCoroutine(UpdateEnginePitch());
        }
    }

    private IEnumerator UpdateEnginePitch()
    {
        while (true)
        {
            currentSpeed = plane.currentSpeed;
            normalizedSpeed = Mathf.InverseLerp(minSpeed, maxSpeed, currentSpeed);
            pitch = Mathf.Lerp(minPitch, maxPitch, normalizedSpeed);
            engineSound[4].pitch = pitch;

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ExecuteExplode() => engineSound[4].Stop();
}
