using PlaneSection;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public sealed class ExecuteFinishObservers : MonoBehaviour
{
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
    public void Constuctor(FinishLine finishLine) => this.finishLine = finishLine;

    private void OnTriggerEnter()
    {
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
        waitForTimeIndicator.enabled = false;
        if (!isReachedLosePoint)
            finishLine.NotifyObserversAboutFinish();
        else
            SceneManager.RestartScene();
    }
}
