using TMPro;
using UnityEngine;

public class PurchaseInfo : MonoBehaviour
{
    [SerializeField] private GameObject noAdButton;
    [SerializeField] private TextMeshProUGUI crystalCount;

    private const string IsAdRemoved = nameof(IsAdRemoved);
    private const string Crystalls = nameof(Crystalls);

    private void Start()
    {
        UpdateRemoveAdButton();
        UpdateCrystalCount();
    }

    public void UpdateRemoveAdButton()
    {
        bool isAdButtonRemoved = PlayerPrefs.GetInt(IsAdRemoved, 0) == 1;
        noAdButton.SetActive(!isAdButtonRemoved);
    }

    public void UpdateCrystalCount()
    {
        int currentCrystalCount = PlayerPrefs.GetInt(Crystalls, 0);
        crystalCount.text = currentCrystalCount.ToString();
    }
}
