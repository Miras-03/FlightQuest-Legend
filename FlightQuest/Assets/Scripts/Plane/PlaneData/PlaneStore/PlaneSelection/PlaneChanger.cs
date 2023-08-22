using UnityEngine;

public sealed class PlaneChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] planeData;

    [SerializeField] private PlaneDisplay planeDisplay;

    private const string SelectedPlane = nameof(SelectedPlane);
    private int currentIndex = 0;

    private void Start()
    {
        currentIndex = PlayerPrefs.GetInt(SelectedPlane, 0);
        ChangePlaneData(currentIndex);
    }

    public void ChangePlaneData(int change)
    {
        currentIndex += change;

        if (currentIndex < 0) 
            currentIndex = planeData.Length - 1;
        else if (currentIndex > planeData.Length - 1) 
            currentIndex = 0;

        planeDisplay?.DisplayPlane((PlaneData)planeData[currentIndex]);
    }

    public void BuyPlane() => PlaneSelectionManager.SaveSelectedPlane(currentIndex);
}
