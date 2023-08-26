using UnityEngine;
using Zenject;

public sealed class PlaneChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] planeData;
    private PlaneData currentPlaneData;

    [SerializeField] private PlaneDisplay planeDisplay;
    private StoreButtonController storeButton;

    private const string SelectedPlane = nameof(SelectedPlane);
    private int currentIndex = 0;

    [Inject]
    public void Construct(StoreButtonController storeButton) => this.storeButton = storeButton;

    private void Start()
    {
        currentIndex = PlayerPrefs.GetInt(SelectedPlane, 0);
        ChangePlaneData(0);
    }

    public void ChangePlaneData(int change)
    {
        currentIndex += change;

        if (currentIndex < 0) 
            currentIndex = planeData.Length - 1;
        else if (currentIndex > planeData.Length - 1) 
            currentIndex = 0;

        currentPlaneData = (PlaneData)planeData[currentIndex];
        storeButton.UpdateButtonInteractable(currentPlaneData, currentIndex);

        planeDisplay?.DisplayPlane((PlaneData)planeData[currentIndex]);
    }

    public void AcquirePlane() => storeButton.BuyPlane(currentPlaneData, currentIndex);
}
