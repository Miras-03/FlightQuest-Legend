using System.Collections;
using UnityEngine;
using Zenject;

public sealed class LosePoint : MonoBehaviour
{
    private ExecuteFinishObservers executeFinishObserver;

    private void Awake() => executeFinishObserver = GetComponentInParent<ExecuteFinishObservers>();

    private void OnTriggerEnter()
    {
        executeFinishObserver.isReached = true;
        executeFinishObserver.waitForTimeIndicator.enabled = false;
    }
}
