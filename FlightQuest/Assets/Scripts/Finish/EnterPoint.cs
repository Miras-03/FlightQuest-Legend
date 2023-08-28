using System.Collections;
using UnityEngine;
using Zenject;

public sealed class EnterPoint : MonoBehaviour
{
    private SceneManager sceneManager;
    private ExecuteFinishObservers executeFinishObserver;

    [Inject]
    public void Constructor(SceneManager sceneManager)
    {
        this.sceneManager = sceneManager;
        executeFinishObserver = GetComponentInParent<ExecuteFinishObservers>();
    }

    private void OnTriggerEnter()=> StartCoroutine(WaitForLose());

    private IEnumerator WaitForLose()
    {
        yield return new WaitForSeconds(20);
        if (!executeFinishObserver.isReachedFinishPoint)
            sceneManager.RestartScene();
    }
}
