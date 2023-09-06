using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public sealed class ExecuteFinishObservers : MonoBehaviour, IDieable
{
    private SceneManager sceneManager;
    private FinishLine finishLine;

    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [HideInInspector] public TextMeshProUGUI waitForTimeIndicator;

    private bool gotIn = false;
    [HideInInspector] public bool isReachedLosePoint = false;
    [HideInInspector] public bool isReachedFinishPoint = false;

    private int time = 5;
    private const int endTime = 0;
    private const int perSeconds = 1;

    [Inject]
    public void Constuctor(SceneManager sceneManager, FinishLine finishLine)
    {
        this.sceneManager = sceneManager;
        this.finishLine = finishLine;
    }

    private void OnTriggerEnter()
    {
        LandingGear.hasExit = true;
        if (!gotIn)
        {
            DestroyFinishLine();
            StartCoroutine(WaitForDelay());
        }
    }

    private void DestroyFinishLine()
    {
        isReachedFinishPoint = true;

        Destroy(meshRenderer);
        Destroy(spriteRenderer);
    }

    private IEnumerator WaitForDelay()
    {
        gotIn = true;
        waitForTimeIndicator.enabled = true;

        while (time > endTime)
        {
            waitForTimeIndicator.text = $"Wait for {time} sec";
            yield return new WaitForSeconds(1);
            time -= perSeconds;
        }
        TurnOffText();
        if (!isReachedLosePoint)
            finishLine.NotifyObserversAboutFinish();
        else
            sceneManager.RestartScene();
    }

    public void TurnOffText() => waitForTimeIndicator.enabled = false;

    public void ExecuteExplode()
    {
        TurnOffText();
        isReachedLosePoint = !isReachedLosePoint;
    }
}
