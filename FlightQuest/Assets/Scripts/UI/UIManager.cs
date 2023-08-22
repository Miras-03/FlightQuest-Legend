using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour, IFinishable, IDieable
{
    [SerializeField] private LevelManager levelManager;

    [Space(20)]
    [Header("UITexts")]
    [SerializeField] private TextMeshProUGUI levelIndicator;

    [Space(20)]
    [Header("UIObjects")]
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private GameObject[] objectsOfUI;
    [SerializeField] private GameObject[] objectsOfStartUI;

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

    public void SetUIObjects()
    {
        objectsOfStartUI[0].SetActive(false);
        objectsOfStartUI[1].SetActive(true);
    }
}
