using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

public class Purchaser : MonoBehaviour
{
    private CrystalManager moneyManager;
    private PurchaseInfo purchaseInfo;

    private const string noAd = "gamepurchases.removead";
    private const string gamePurchases = "gamepurchases.getcrystalls";

    private const string IsAdRemoved = nameof(IsAdRemoved);
    private const string Crystalls = nameof(Crystalls);

    private const int crystallsCount = 3000;

    [Inject]
    public void Constructor(CrystalManager moneyManager, PurchaseInfo purchaseInfo)
    {
        this.moneyManager = moneyManager;
        this.purchaseInfo = purchaseInfo;
    }

    public void OnPurchaseCompleted(Product product)
    {
        switch (product.definition.id)
        {
            case noAd:
                RemoveAd();
                break;
            case gamePurchases:
                GetCrystalls();
                break;
        }
    }

    private void GetCrystalls()
    {
        moneyManager.TakeMoney(crystallsCount);
        purchaseInfo.UpdateCrystalCount();
    }

    private void RemoveAd()
    {
        PlayerPrefs.SetInt(IsAdRemoved, 1);
        purchaseInfo.UpdateRemoveAdButton();
    }
}
