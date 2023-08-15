using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterPoint : MonoBehaviour
{
    private ExecuteFinishObservers executeFinishObserver;

    private void Awake() => executeFinishObserver = GetComponentInParent<ExecuteFinishObservers>();

    private void OnTriggerEnter()=> StartCoroutine(WaitForLose());

    private IEnumerator WaitForLose()
    {
        yield return new WaitForSeconds(10);
        if (!executeFinishObserver.isReachedFinishLine)
            SceneManager.RestartScene();
    }
}
