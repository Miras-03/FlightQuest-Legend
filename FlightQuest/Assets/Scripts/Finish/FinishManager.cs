using PlaneSection;
using UnityEngine;
using Zenject;

public sealed class FinishManager : MonoBehaviour
{
    private FinishLine finishLine;
    [SerializeField] private SpeedManager speedManager;
    [SerializeField] private ExecuteFinishObservers confetti;
    private MeshRenderer meshRenderer;

    [Inject]
    public void Constructor(FinishLine finishLine)
    {
        this.finishLine = finishLine;

        finishLine.AddObservers(speedManager);
        finishLine.AddObservers(confetti);

        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnDisable() => finishLine.RemoveObservers();

    private void OnTriggerEnter()
    {
        finishLine.NotifyObserversAboutFinish();
        meshRenderer.enabled = false;
    }
}