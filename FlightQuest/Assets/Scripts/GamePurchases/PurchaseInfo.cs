using TMPro;
using UnityEngine;
using Zenject;

public class PurchaseInfo : MonoBehaviour
{
    [SerializeField] private GameObject noAdButton;
    private TextMeshProUGUI crystalCount;

    private const string IsAdRemoved = nameof(IsAdRemoved);
    private const string Crystalls = nameof(Crystalls);

    [Inject]
    public void Construct(TextMeshProUGUI crystalCount) => this.crystalCount = crystalCount;

    private void Start()
    {
        UpdateRemoveAdButton();
        UpdateCrystalCount();
    }

    public void UpdateRemoveAdButton()
    {
        if (noAdButton != null)
        {
            bool isAdButtonRemoved = PlayerPrefs.GetInt(IsAdRemoved, 0) == 1;
            noAdButton.SetActive(!isAdButtonRemoved);
        }
    }

    public void UpdateCrystalCount()
    {
        int currentCrystalCount = PlayerPrefs.GetInt(Crystalls, 0);
        crystalCount.text = currentCrystalCount.ToString();
    }
}
