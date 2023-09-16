using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    private InterstitialAd interstitialAd;

    private const string androidGameId = "5403455";
    private const string iOSGameId = "5403454";

    [SerializeField] private bool testMode = true;
    private string _gameId;

    private void Awake()
    {
        interstitialAd = GetComponent<InterstitialAd>();

        InitializeAds();
    }

    public void InitializeAds()
    {
    #if UNITY_IOS
            _gameId = _iOSGameId;
    #elif UNITY_ANDROID
            _gameId = androidGameId;
    #elif UNITY_EDITOR
            _gameId = _androidGameId;
    #endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
            Advertisement.Initialize(_gameId, testMode, this);
    }

    public void OnInitializationComplete() => interstitialAd.LoadAd();

    public void OnInitializationFailed(UnityAdsInitializationError error, string message) { }
}