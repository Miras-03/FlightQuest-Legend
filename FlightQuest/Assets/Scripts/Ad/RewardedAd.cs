using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using Zenject;

public class RewardedAdButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    private CurrencyManager currencyManager;

    [SerializeField] Button showAdButton;

    private const int rewardedAmount = 10200;

    private const string _androidAdUnitId = "Rewarded_Android";
    private const string _iOSAdUnitId = "Rewarded_iOS";

    private string _adUnitId = null;

    [Inject]
    public void Construct(CurrencyManager currencyManager)
    {
        this.currencyManager = currencyManager;

        InitializeAds();
        StartCoroutine(WaitForLoad());
    }

    private void InitializeAds()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = _androidAdUnitId;
#endif

        showAdButton.interactable = false;
    }

    public void LoadAd() => Advertisement.Load(_adUnitId, this);

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        if (adUnitId.Equals(_adUnitId))
        {
            showAdButton.onClick.AddListener(ShowAd);
            showAdButton.interactable = true;
        }
    }

    public void ShowAd()
    {
        showAdButton.interactable = false;
        Advertisement.Show(_adUnitId, this);
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (currencyManager != null && adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
            currencyManager.TakeCurrency(rewardedAmount);
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message) { }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message) { }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy() => showAdButton.onClick.RemoveAllListeners();

    private IEnumerator WaitForLoad()
    {
        yield return new WaitForSeconds(3);
        LoadAd();
    }
}