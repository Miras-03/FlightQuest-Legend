using TMPro;
using UnityEngine;
using Zenject;

public class CurrencyManager : MonoBehaviour
{
    private Currency currency;

    [SerializeField] private TextMeshProUGUI showText;

    private int ordinaryCurrency = 3400;
    private int doubleCurrency = 10200;

    [Inject]
    public void Constructor(Currency currency) => this.currency = currency;

    private void Start() => ShowCurrencyText();

    public void ShowCurrencyText() => currency.ShowCurrencyCount(showText);

    public void GetCurrency() => currency.TakeCurrency += ordinaryCurrency;
    public void GetDoubleCurrency() => currency.TakeCurrency += doubleCurrency;
}
