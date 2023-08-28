using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class StoreButtonController : MonoBehaviour
{
    private CurrencyManager currencyManager;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI currentState;
    [SerializeField] private Button button;

    private AudioSource[] buySound;

    private int selectedPlaneIndex;

    private const string _IsBought = nameof(_IsBought);

    private const string SelectedPlane = nameof(SelectedPlane);
    private const string Get = nameof(Get);
    private const string Buy = nameof(Buy);

    [Inject]
    public void Construct(CurrencyManager currencyManager, AudioSource[] buySound)
    {
        this.currencyManager = currencyManager;
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

        button.interactable = false;
        SavePlane(currentPlaneData, currentIndex);
        ChangeCurrentState(isBought, currentIndex);
        UpdateButtonInteractable(currentPlaneData, currentIndex);
    }

    private void ChangeCurrentState(bool isBought, int currentIndex)
    {
        if (!isBought && currentIndex != selectedPlaneIndex)
            currentState.text = Buy;
        else
            currentState.text = Get;
    }

    public void UpdateButtonInteractable(PlaneData currentPlaneData, int currentIndex)
    {
        bool isBought = button.interactable = CheckForEnsure(currentPlaneData, currentIndex);
        ChangeCurrentState(isBought, currentIndex);

        if (isBought || currencyManager.GetCurrency >= currentPlaneData.price && currentIndex != selectedPlaneIndex)
            button.interactable = true;
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
}
