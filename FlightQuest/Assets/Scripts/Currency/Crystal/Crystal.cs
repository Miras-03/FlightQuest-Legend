using TMPro;
using UnityEngine;

public class Crystal
{
    private const string CrystalCount = nameof(CrystalCount);

    public int TakeCrystal
    {
        get => PlayerPrefs.GetInt(CrystalCount, 0);
        set => PlayerPrefs.SetInt(CrystalCount, value);
    }

    public void ShowCrystalCount(TextMeshProUGUI currentText) => currentText.text = $"{TakeCrystal}  ";
}
