using PlaneSection;
using TMPro;
using UnityEngine;
using Zenject;

public class CanisterCountChanger : MonoBehaviour
{
    private TextMeshProUGUI canisterCount;

    private int count = 0;
    private int maxCanisterCount;
    private const int defaultCanisterCount = 10;

    private const string CanisterCount = nameof(CanisterCount);

    [Inject]
    public void Constructor(TextMeshProUGUI[] texts)
    {
        canisterCount = texts[2];

        SetCanisterCount();
        PointDetector.OnCanisterTaken += ChangeCanisterCount;
    }

    private void SetCanisterCount()
    {
        maxCanisterCount = PlayerPrefs.GetInt(CanisterCount, defaultCanisterCount);
        canisterCount.text = $"{count}/{maxCanisterCount}";
    }

    private void ChangeCanisterCount() => canisterCount.text = $"{++count}/{maxCanisterCount}";
}
