using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class StoreButtonController : MonoBehaviour
{
    private CurrencyManager currencyManager;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI currentState;
    [SerializeField] private Button buyButton;

    private const string _IsBought = nameof(_IsBought);

    private const string Get = nameof(Get);
    private const string Buy = nameof(Buy);

    [Inject]
    public void Construct(CurrencyManager currencyManager) => this.currencyManager = currencyManager;

    public void BuyPlane(PlaneData currentPlaneData, int currentIndex)
    {
        bool isBought = CheckForEnsure(currentPlaneData, currentIndex);

        if (!isBought)
            currencyManager.TakeCurrency(-currentPlaneData.price);

        SavePlane(currentPlaneData, currentIndex);
        UpdateButtonState();
    }

    private void ChangeCurrentState(string state) => currentState.text = state;

    public void UpdateButtonInteractable(PlaneData currentPlaneData, int currentIndex)
    {
        bool isBought = CheckForEnsure(currentPlaneData, currentIndex);

        buyButton.interactable = false;

        ChangeCurrentState(Buy);

        if (isBought)
            ChangeCurrentState(Get);
        if (isBought || currencyManager.GetCurrency >= currentPlaneData.price)
            buyButton.interactable = true;
    }

    private bool CheckForEnsure(PlaneData currentPlaneData, int currentIndex)
    {
        string planeName = currentPlaneData.name;
        return PlayerPrefs.GetInt(planeName + _IsBought, 0) == 1 || currentIndex == 0;
    }

    public void UpdateButtonState()
    {
        buyButton.interactable = false;
        ChangeCurrentState(Get);
    }

    private void SavePlane(PlaneData currentPlaneData, int currentIndex)
    {
        PlaneSelectionManager.SaveSelectedPlane(currentIndex);
        string planeName = currentPlaneData.name;
        PlayerPrefs.SetInt(planeName + _IsBought, 1);
        PlayerPrefs.Save();
    }
}
