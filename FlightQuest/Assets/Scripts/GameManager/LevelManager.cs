using System;
using UnityEngine;

public sealed class LevelManager : MonoBehaviour, IFinishable
{
    private int currentLevel;

    private const int defaultLevel = 1;

    private const string CurrentLevel = nameof(CurrentLevel);

    private const string CanisterCount = nameof(CanisterCount);
    private const string Distance = nameof(Distance);

    public int GetCurrentLevel { get => currentLevel; }

    private void Awake() => currentLevel = PlayerPrefs.GetInt(CurrentLevel, defaultLevel);

    public void ExecuteFinish() => SaveLevel();

    private void SaveLevel() => PlayerPrefs.SetInt(CurrentLevel, ++currentLevel);
}
