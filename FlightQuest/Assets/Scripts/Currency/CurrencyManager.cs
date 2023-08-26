using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public sealed class CurrencyManager : MonoBehaviour
{
    private Currency currency;

    [SerializeField] private TextMeshProUGUI showText;

    private float smoothChangeDuration = 1.0f;

    [Inject]
    public void Constructor(Currency currency) => this.currency = currency;

    private void Start() => ShowCurrencyText();

    public void ShowCurrencyText() => currency.ShowCurrencyCount(showText);

    public int GetCurrency { get => currency.TakeCurrency; }

    public void TakeCurrency(int amount)
    {
        int initialCurrency = currency.TakeCurrency;

        StartCoroutine(SmoothChangeCurrency(initialCurrency, initialCurrency + amount));

        currency.TakeCurrency += amount;
    }

    private IEnumerator SmoothChangeCurrency(int startValue, int endValue)
    {
        float elapsedTime = 0;

        while (elapsedTime < smoothChangeDuration)
        {
            float currentValue = Mathf.Lerp(startValue, endValue, elapsedTime / smoothChangeDuration);

            showText.text = $"{Mathf.RoundToInt(currentValue)}$";

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        showText.text = $"{endValue}$";
    }
}
