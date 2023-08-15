using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour, IFinishable, IDieable
{
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject[] buttons;

    public void ExecuteFinish()
    {
        DestroyButtons();
        StartCoroutine(EnableUIPanel());
    }

    public void ExecuteExplode() => DestroyButtons();

    private IEnumerator EnableUIPanel()
    {
        yield return new WaitForSeconds(3f);
        finishPanel.SetActive(true);
    }

    private void DestroyButtons()
    {
        foreach (GameObject button in buttons)
            Destroy(button);
    }   
}
