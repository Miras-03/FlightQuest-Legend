using System.Collections;
using TMPro;
using UnityEngine;

public sealed class UIManager : MonoBehaviour, IFinishable, IDieable
{
    [SerializeField] private LevelManager levelManager;

    [Space(20)]
    [Header("UITexts")]
    [SerializeField] private TextMeshProUGUI levelIndicator;

    [Space(20)]
    [Header("UIObjects")]
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject[] objectsOfUI;

    private void Start()
    {
        TurnOnButtons();
        levelIndicator.text = $"Level {levelManager.GetCurrentLevel}";
    }

    public void ExecuteFinish()
    {
        TurnOffButtons();
        StartCoroutine(EnableUIPanel());
    }

    public void ExecuteExplode() => TurnOffButtons();

    private IEnumerator EnableUIPanel()
    {
        yield return new WaitForSeconds(3f);
        finishPanel.SetActive(true);
    }

    private void TurnOffButtons()
    {
        foreach (GameObject obj in objectsOfUI)
            obj.SetActive(false);
    }

    private void TurnOnButtons()
    {
        foreach (GameObject obj in objectsOfUI)
            obj.SetActive(true);
    }
}
