using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private static int currentScene;

    private void Start() => currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;

    public static void RestartScene() => UnityEngine.SceneManagement.SceneManager.LoadScene(currentScene);
}
