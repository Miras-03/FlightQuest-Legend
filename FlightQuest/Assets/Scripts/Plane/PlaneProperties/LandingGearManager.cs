using CameraOption;
using PlaneSection;
using UnityEngine;

public class LandingGearManager : MonoBehaviour
{
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private PointDetector pointDetector;
    [SerializeField] private LandingGear landingGear;

    private bool isEntered = false;

    private void OnTriggerEnter()
    {
        if (!isEntered)
        {
            landingGear.SetLandingGear();
            cameraManager.ChangeCameraCoord();
            Destroy(pointDetector);
            isEntered = !isEntered;
        }
    }
}
