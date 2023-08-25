using CameraOption;
using PlaneSection;
using System.Collections;
using UnityEngine;
using Zenject;

public sealed class PlaneSelectionManager : MonoBehaviour
{
    private DiContainer container;

    private PlaneData currentPlaneData;
    private PlaneController planeController;
    private PlaneControl planeControl;
    private AirPlane plane;

    private CameraManager cameraManager;

    [SerializeField] private ScriptableObject[] planeData;

    private const string SelectedPlane = nameof(SelectedPlane);
    private int index = 0;

    [Inject]
    public void Constructor(DiContainer container, CameraManager cameraManager)
    {
        this.container = container;
        this.cameraManager = cameraManager;
    }

    private void Start() => LoadSelectedPlane();

    public static void SaveSelectedPlane(int index) => PlayerPrefs.SetInt(SelectedPlane, index);

    private void LoadSelectedPlane()
    {
        index = PlayerPrefs.GetInt(SelectedPlane, 0);
        currentPlaneData = (PlaneData)planeData[index];

        GameObject planeInstance = container.InstantiatePrefab(currentPlaneData.planeModel);

        InjectAfterDelay();

        cameraManager.OnPlaneInstanceReady(planeInstance);

        var notifier = GetComponent<PrefabInitializationNotifier>();
        StartCoroutine(NotifyAfterDelay(notifier));
    }

    private void InjectAfterDelay()
    {
        planeControl = FindObjectOfType<PlaneControl>();
        planeController = FindObjectOfType<PlaneController>();
        plane = FindObjectOfType<AirPlane>();

        plane.maxPossibleSpeed = currentPlaneData.speed;
        plane.speedAcceleration = currentPlaneData.acceleration;
        plane.decelerationFactor = currentPlaneData.deceleration;

        planeControl.rotationSmoothSpeed = currentPlaneData.rotationSmoothSpeed;

        planeControl.yawAmount = currentPlaneData.yawAmount;
        planeController.pitchAmount = currentPlaneData.pitchAmount;
        planeController.groundPitchAmount = currentPlaneData.groundPitchAmount;
        planeController.rollAmount = currentPlaneData.rollAmount;
    }

    private IEnumerator NotifyAfterDelay(PrefabInitializationNotifier notifier)
    {
        yield return new WaitForSeconds(1f);
        notifier.NotifyPrefabInitialized();
    }
}