using PlaneSection;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class UIAnimationManager : MonoBehaviour
{
    public event Action OnUIElementsTurnedOn;

    [Header("UIElements")]
    [SerializeField] private GameObject startUIElements;
    [SerializeField] private GameObject cursor;
    [SerializeField] private GameObject indicators;

    [Space(20)]
    [Header("Animators")]
    [SerializeField] private Animator[] animators;

    private bool injected = false;

    [Inject]
    public void Initialize(PrefabInitializationNotifier notifier) =>
        notifier.OnPrefabInitialized += InjectAfterDelay;

    private void InjectAfterDelay()
    {
        if (!injected)
        {
            injected = true;

            SpeedManager.OnShiftUIElements += SetUIElements;
        }
    }

    private void OnDisable() => SpeedManager.OnShiftUIElements -= SetUIElements;

    private void SetUIElements()
    {
        cursor.SetActive(false);
        foreach (Animator anim in animators)
            anim.enabled = true;
        StartCoroutine(WaitForSetUI());
    }

    private IEnumerator WaitForSetUI()
    {
        yield return new WaitForSeconds(0.8f);

        startUIElements.SetActive(false);
        indicators.SetActive(true);

        OnUIElementsTurnedOn.Invoke();
    }
}
