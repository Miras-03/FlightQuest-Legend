using UnityEngine;
using UnityEngine.UI;

public sealed class PetrolLevel : MonoBehaviour
{
    private Slider petrolLevel;
    [SerializeField] private Image fillImage;

    private Animator animator;

    private Color fullColor = Color.green;
    private Color mediumColor = Color.yellow;
    private Color lowColor = Color.red;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        petrolLevel = GetComponent<Slider>();
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
        float value = petrolLevel.value;

        if (value < 30 && value > 2)
            animator.enabled = true;
        else
            animator.enabled = false;
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
