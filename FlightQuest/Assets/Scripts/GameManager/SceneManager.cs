using System.Collections;
using UnityEngine;
using Zenject;

public sealed class SceneManager : MonoBehaviour
{
    private InterstitialAd interstitialAd;

    [SerializeField] private Animator fadeAnimator;
    private int currentScene;

    private const string FadeOutTrigger = "FadeOut";
    private const string IsAdRemoved = nameof(IsAdRemoved);

    [Inject]
    public void Construct(InterstitialAd interstitialAd) => this.interstitialAd = interstitialAd; 

    private void Start() => currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

    public void RestartScene()
    {
        if (!PlayerPrefs.HasKey(IsAdRemoved))
            interstitialAd.ShowAd();
        StartCoroutine(WaitForLoad(currentScene));
    }

    public void LoadScene(int sceneIndex) => StartCoroutine(WaitForLoad(sceneIndex));

    private void FadeOut() => fadeAnimator.SetTrigger(FadeOutTrigger);

    private IEnumerator WaitForLoad(int index)
    {
        FadeOut();
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}
