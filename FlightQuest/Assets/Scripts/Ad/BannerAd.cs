using UnityEngine;
using UnityEngine.Advertisements;

public class BannerAd : MonoBehaviour
{
    private BannerPosition bannerPosition = BannerPosition.BOTTOM_CENTER;

    private const string androidAdUnitId = "Banner_Android";
    private const string iOSAdUnitId = "Banner_iOS";

    private const string LoadBannerText = "LoadBanner";

    private string _adUnitId = null; 

    void Start()
    {
#if UNITY_IOS
        _adUnitId = _iOSAdUnitId;
#elif UNITY_ANDROID
        _adUnitId = androidAdUnitId;
#endif

        Advertisement.Banner.SetPosition(bannerPosition);

        Invoke(LoadBannerText, 1);
    }

    public void LoadBanner()
    {
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        Advertisement.Banner.Load(_adUnitId, options);
    }

    void OnBannerLoaded() => ShowBannerAd();

    void OnBannerError(string message) { }

    void ShowBannerAd()
    {
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        Advertisement.Banner.Show(_adUnitId, options);
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }
}