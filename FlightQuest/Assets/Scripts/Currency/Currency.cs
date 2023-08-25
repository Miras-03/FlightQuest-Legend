using TMPro;
using UnityEngine;

public class Currency
{
    private const string CurrencyCount = nameof(CurrencyCount);

    public int TakeCurrency
    {
        get => PlayerPrefs.GetInt("CurrencyCount");
        set => PlayerPrefs.SetInt("CurrencyCount", value);
    }

    public void ShowCurrencyCount(TextMeshProUGUI currentText) => currentText.text = $"{TakeCurrency}$";
}
