using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public sealed class CrystalManager : MonoBehaviour
{
    private Crystal money;

    [SerializeField] private TextMeshProUGUI showText;

    private float smoothChangeDuration = 1.0f;

    [Inject]
    public void Constructor(Crystal money) => this.money = money;

    private void Start() => ShowMoneyText();

    public void ShowMoneyText() => money.ShowCrystalCount(showText);

    public int GetMoney { get => money.TakeCrystal; }

    public void TakeMoney(int amount)
    {
        int initialMoney = money.TakeCrystal;

        StartCoroutine(SmoothChangeMoney(initialMoney, initialMoney + amount));

        money.TakeCrystal += amount;
    }

    private IEnumerator SmoothChangeMoney(int startValue, int endValue)
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
