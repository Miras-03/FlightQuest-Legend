using TMPro;
using UnityEngine;

public sealed class LevelManager : MonoBehaviour, IFinishable
{
    private int currentLevel;
    public int GetCurrentLevel { get => currentLevel; }
    private void Awake() => currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
    public void ExecuteFinish() => PlayerPrefs.SetInt("CurrentLevel", ++currentLevel);
}
