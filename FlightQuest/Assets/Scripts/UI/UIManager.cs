using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour, IFinishable
{
    [SerializeField] private GameObject finishPanel;

    public void ExecuteFinish() => StartCoroutine(EnableUIPanel());

    private IEnumerator EnableUIPanel()
    {
        yield return new WaitForSeconds(3f);
        finishPanel.SetActive(true);
    }
}
