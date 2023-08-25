using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class StoreButtonController : MonoBehaviour
{
    private Currency currency;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI currentState;
    [SerializeField] private Button buyButton;

    private const string _IsBought = nameof(_IsBought);

    [Inject]
    public void Construct(Currency currency) => this.currency = currency;

    public void BuyPlane(PlaneData currentPlaneData, int currentIndex)
    {
        bool isBought = CheckForEnsure(currentPlaneData);

        if(!isBought) 
            currency.TakeCurrency -= currentPlaneData.price;

        SavePlane(currentPlaneData, currentIndex);
    }

    public void UpdateButtonInteractable(PlaneData currentPlaneData)
    {
        bool isBought = CheckForEnsure(currentPlaneData);

        buyButton.interactable = false;

        currentState.text = "Buy";

        if (isBought)
            currentState.text = "Get";
        if (isBought || currency.TakeCurrency >= currentPlaneData.price)
            buyButton.interactable = true;
    }

    private bool CheckForEnsure(PlaneData currentPlaneData)
    {
        string planeName = currentPlaneData.name;
        return PlayerPrefs.GetInt(planeName + _IsBought, 0) == 1;
    }

    private void SavePlane(PlaneData currentPlaneData, int currentIndex)
    {
        PlaneSelectionManager.SaveSelectedPlane(currentIndex);
        string planeName = currentPlaneData.name;
        PlayerPrefs.SetInt(planeName + _IsBought, 1);
        PlayerPrefs.Save();
    }
}
