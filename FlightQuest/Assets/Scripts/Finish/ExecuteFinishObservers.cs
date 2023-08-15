using PlaneSection;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public sealed class ExecuteFinishObservers : MonoBehaviour
{
    [SerializeField] private PointDetector pointDetector;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public TextMeshProUGUI waitForTimeIndicator;

    [Inject] private FinishLine finishLine;
    private bool gotIn = false;
    [HideInInspector] public bool isReached = false;
    [HideInInspector] public bool isReachedFinishLine = false;

    private int time = 5;
    private const int endTime = 0;
    private const int perSeconds = 1;

    [Inject]
    public void Constuctor(FinishLine finishLine) => this.finishLine = finishLine;

    private void OnTriggerEnter(Collider other)
    {
        pointDetector.BreakDown();
        
        if (!gotIn)
        {
            DestroyFinishLine();
            StartCoroutine(WaitForDelay());
        }
    }

    private void DestroyFinishLine()
    {
        isReachedFinishLine = true;

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

        if (!isReached)
            finishLine.NotifyObserversAboutFinish();
        else
            SceneManager.RestartScene();
    }
}
