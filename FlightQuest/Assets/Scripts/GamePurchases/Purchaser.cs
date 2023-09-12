using UnityEngine;
using UnityEngine.Purchasing;
using Zenject;

public class Purchaser : MonoBehaviour
{
    private AudioSource moneySound;

    private CrystalManager moneyManager;
    private PurchaseInfo purchaseInfo;

    private const string noAd = "gamepurchases.removead";
    private const string gamePurchases = "gamepurchases.getcrystals";

    private const string IsAdRemoved = nameof(IsAdRemoved);
    private const string Crystalls = nameof(Crystalls);

    private const int crystallsCount = 3000;

    [Inject]
    public void Constructor(CrystalManager moneyManager, PurchaseInfo purchaseInfo, AudioSource[] audioSource)
    {
        this.moneyManager = moneyManager;
        this.purchaseInfo = purchaseInfo;
        moneySound = audioSource[0];
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
        moneySound.Play();
        moneyManager.TakeMoney(crystallsCount);
        purchaseInfo.UpdateCrystalCount();
    }

    private void RemoveAd()
    {
        PlayerPrefs.SetInt(IsAdRemoved, 1);
        purchaseInfo.UpdateRemoveAdButton();
    }
}
