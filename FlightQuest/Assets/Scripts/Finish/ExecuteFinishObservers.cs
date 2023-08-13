using System.Collections;
using UnityEngine;

public class ExecuteFinishObservers : MonoBehaviour, IFinishable
{
    [SerializeField] private ParticleSystem confetti;
    [SerializeField] private GameObject finishUIPanel;

    public void Execute()
    {
        confetti.Play();
        StartCoroutine(EnableUIPanel());
    }

    private IEnumerator EnableUIPanel()
    {
        yield return new WaitForSeconds(3f);
        finishUIPanel.SetActive(true);
    }
}
