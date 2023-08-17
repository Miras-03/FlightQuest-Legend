using UnityEngine;
using UnityEngine.UI;

public class PetrolLevel : MonoBehaviour
{
    private Slider petrolLevel;
    private Image fillImage;

    private Color fullColor = Color.green;
    private Color mediumColor = Color.yellow;
    private Color lowColor = Color.red;

    private void Awake()
    {
        petrolLevel = GetComponent<Slider>();
        fillImage = petrolLevel.fillRect.GetComponent<Image>();
    }

    public int SetMaxLevel
    {
        set
        {
            petrolLevel.maxValue = value;
            petrolLevel.value = value;
            UpdateColor();
        }
    }

    public void SetPetrolLevel(float level)
    {
        petrolLevel.value -= level;
        UpdateColor();
    }

    private void UpdateColor()
    {
        float fillPercentage = petrolLevel.value / petrolLevel.maxValue;

        if (fillPercentage > 0.5f)
            fillImage.color = Color.Lerp(mediumColor, fullColor, (fillPercentage - 0.5f) * 2f);
        else
            fillImage.color = Color.Lerp(lowColor, mediumColor, fillPercentage * 2f);
    }
}
