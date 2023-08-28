using System;
using System.Collections;
using UnityEngine;

public sealed class SceneManager : MonoBehaviour
{
    [SerializeField] private Animator fadeAnimator;
    private static int currentScene;

    private void Start() => currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

    public void RestartScene() => StartCoroutine(WaitForLoad(currentScene));

    public void LoadScene(int sceneIndex) => StartCoroutine(WaitForLoad(sceneIndex));

    private void FadeOut() => fadeAnimator.SetTrigger("FadeOut");

    private IEnumerator WaitForLoad(int index)
    {
        FadeOut();
        yield return new WaitForSeconds(1);
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}
