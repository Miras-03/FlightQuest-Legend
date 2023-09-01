using PlaneSection;
using System.Collections;
using UnityEngine;
using Zenject;

public sealed class EngineSoundController : MonoBehaviour, IDieable
{
    private AudioSource engineSound;
    private AirPlane plane;

    private int planeIndex;
    private const int startSoundPosition = 4;
    private const string SelectedPlane = nameof(SelectedPlane);

    private float pitch;

    private const float minPitch = 0.5f;
    private float maxPitch = 3f;

    private float currentSpeed;
    private float normalizedSpeed;
    private const float minSpeed = 0f;
    private float maxSpeed;

    private bool injected = false;

    [Inject]
    public void Initialize(PrefabInitializationNotifier notifier, AudioSource[] engineSound)
    {
        notifier.OnPrefabInitialized += InjectAfterDelay;

        planeIndex = PlayerPrefs.GetInt(SelectedPlane, startSoundPosition) + startSoundPosition;
        this.engineSound = engineSound[planeIndex];
        SetMaxPitch();
    }

    private void InjectAfterDelay()
    {
        if (!injected)
        {
            injected = true;

            plane = GetComponent<AirPlane>();
            maxSpeed = plane.maxPossibleSpeed;

            engineSound.Play();
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
            engineSound.pitch = pitch;

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void ExecuteExplode() => engineSound.Stop();

    private void SetMaxPitch()
    {
        if (planeIndex == 5)
            maxPitch = 1.7f;
    }
}
