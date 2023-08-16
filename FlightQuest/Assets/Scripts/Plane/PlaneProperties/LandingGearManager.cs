using CameraOption;
using UnityEngine;

public class LandingGearManager : MonoBehaviour
{
    [SerializeField] CameraManager cameraManager;
    [SerializeField] private LandingGear landingGear;

    private bool isEntered = false;

    private void OnTriggerEnter()
    {
        if (!isEntered)
        {
            landingGear.SetLandingGear();
            cameraManager.ChangeCameraCoord();
            isEntered = !isEntered;
        }
    }
}
