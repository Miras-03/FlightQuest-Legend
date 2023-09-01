using System;
using System.Collections;
using UnityEngine;

public sealed class SceneManager : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;
    private int currentScene;

    private const string FadeOutTrigger = "FadeOut";

    private void Start() => currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

    public void RestartScene() => StartCoroutine(WaitForLoad(currentScene));

    public void LoadScene(int sceneIndex) => StartCoroutine(WaitForLoad(sceneIndex));

    private void FadeOut() => fadeAnimator.SetTrigger(FadeOutTrigger);

    private IEnumerator WaitForLoad(int index)
    {
        FadeOut();
        yield return new WaitForSeconds(2);
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}
