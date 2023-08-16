using UnityEngine;

public sealed class LosePoint : MonoBehaviour
{
    [SerializeField] private ExecuteFinishObservers executeFinishObserver;

    private void OnTriggerEnter()
    {
        executeFinishObserver.isReachedLosePoint = true;
        executeFinishObserver.waitForTimeIndicator.enabled = false;
    }
}
