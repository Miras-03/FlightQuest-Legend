using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IFinishable, IDieable
{
    [SerializeField] private LevelManager levelManager;

    [SerializeField] private TextMeshProUGUI levelIndicator;
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject[] objectsOfUI;

    private void Start() => levelIndicator.text = $"Level {levelManager.GetCurrentLevel}";

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
        foreach (GameObject button in objectsOfUI)
            Destroy(button);
    }   
}
