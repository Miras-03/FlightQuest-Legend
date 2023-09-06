using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class StoreButtonController : MonoBehaviour
{
    private CurrencyManager currencyManager;
    private CrystalManager moneyManager;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI currentPrice;
    [SerializeField] private TextMeshProUGUI textOfBuyButton;
    [SerializeField] private Button buyButton;

    private AudioSource[] buySound;

    private int selectedPlaneIndex;

    private const string _IsBought = nameof(_IsBought);

    private const string SelectedPlane = nameof(SelectedPlane);
    private const string Get = nameof(Get);
    private const string Buy = nameof(Buy);

    [Inject]
    public void Construct(CurrencyManager currencyManager, CrystalManager moneyManager, AudioSource[] buySound)
    {
        this.currencyManager = currencyManager;
        this.moneyManager = moneyManager;
        this.buySound = buySound;

        selectedPlaneIndex = PlayerPrefs.GetInt(SelectedPlane, 0);
    }

    public void BuyPlane(PlaneData currentPlaneData, int currentIndex)
    {
        bool isBought = CheckForEnsure(currentPlaneData, currentIndex);

        if (!isBought)
        {
            buySound[0].Play();
            currencyManager.TakeCurrency(-currentPlaneData.price);
        }

        buyButton.interactable = false;
        SavePlane(currentPlaneData, currentIndex);
        ChangeCurrentState(isBought, currentIndex);
        UpdateButtonInteractable(currentPlaneData, currentIndex);
    }

    private void ChangeCurrentState(bool isBought, int currentIndex)
    {
        if (!isBought && currentIndex != selectedPlaneIndex)
            textOfBuyButton.text = Buy;
        else
        {
            textOfBuyButton.text = Get;
            ChangeTextColor(Color.green);
        }
    }

    public void UpdateButtonInteractable(PlaneData currentPlaneData, int currentIndex)
    {
        ChangeTextColor(Color.red);

        bool isBought = buyButton.interactable = CheckForEnsure(currentPlaneData, currentIndex);
        ChangeCurrentState(isBought, currentIndex);

        if (currentIndex != selectedPlaneIndex)
        {
            if (!currentPlaneData.gamePurchase && (isBought || currencyManager.GetCurrency >= currentPlaneData.price))
            {
                buyButton.interactable = true;
                ChangeTextColor(Color.green);
            }
            else if (currentPlaneData.gamePurchase && (isBought || moneyManager.GetMoney >= currentPlaneData.price))
            {
                buyButton.interactable = true;
                ChangeTextColor(Color.green);
            }
        }
    }

    private bool CheckForEnsure(PlaneData currentPlaneData, int currentIndex)
    {
        selectedPlaneIndex = PlayerPrefs.GetInt(SelectedPlane, 0);
        string planeName = currentPlaneData.name;
        return (PlayerPrefs.GetInt(planeName + _IsBought, 0) == 1) && currentIndex != selectedPlaneIndex;
    }

    private void SavePlane(PlaneData currentPlaneData, int currentIndex)
    {
        PlaneSelectionManager.SaveSelectedPlane(currentIndex);
        string planeName = currentPlaneData.name;
        PlayerPrefs.SetInt(planeName + _IsBought, 1);
        PlayerPrefs.Save();
    }

    private void ChangeTextColor(Color color) => currentPrice.color = color;
}