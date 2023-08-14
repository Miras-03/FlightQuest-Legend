using UnityEngine;
using Zenject;

public class ExecuteFinishObserver : MonoBehaviour, IFinishable
{
    private FinishLine finishLine;
    [SerializeField] private GameObject finishLineObject;

    [Inject]
    public void Constuctor(FinishLine finishLine) => this.finishLine = finishLine;

    private void OnTriggerEnter() => finishLine.NotifyObserversAboutFinish();

    public void ExecuteFinish() => Destroy(finishLineObject);
}
