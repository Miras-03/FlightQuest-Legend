using PlaneSection;
using UnityEngine;
using Zenject;

public sealed class FinishManager : MonoBehaviour
{
    [Header("FinishObservers")]
    [SerializeField] private SpeedManager speedManager;
    [SerializeField] private ConfettiManager confetti;
    [SerializeField] private UIManager managerUI;
    [SerializeField] private ExecuteFinishObserver finishLineObject;

    private FinishLine finishLine;

    [Inject]
    public void Constructor(FinishLine finishLine)
    {
        this.finishLine = finishLine;

        finishLine.AddObservers(speedManager);
        finishLine.AddObservers(confetti);
        finishLine.AddObservers(managerUI);
        finishLine.AddObservers(finishLineObject);
    }

    private void OnDisable() => finishLine.RemoveObservers();
}