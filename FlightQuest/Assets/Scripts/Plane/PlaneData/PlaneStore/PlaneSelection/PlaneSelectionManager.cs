using CameraOption;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

public sealed class PlaneSelectionManager : MonoBehaviour
{
    private DiContainer container;

    private CameraManager cameraManager;

    [SerializeField] private SelectedPlaneData selectedPlane;
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

    public void SelectPlane(int index)
    {
        PlaneData currentPlaneData = (PlaneData)planeData[index];

        selectedPlane.planeModel = currentPlaneData.planeModel;
        selectedPlane.speed = currentPlaneData.speed;
        selectedPlane.acceleration = currentPlaneData.acceleration;
        selectedPlane.yawAmount = currentPlaneData.yawAmount;
        selectedPlane.pitchAmount = currentPlaneData.pitchAmount;
        selectedPlane.pitchGroundAmount = currentPlaneData.groundPitchAmount;
        selectedPlane.rollAmount = currentPlaneData.rollAmount;
    }

    public static void SaveSelectedPlane(int index) => PlayerPrefs.SetInt(SelectedPlane, index);

    private void LoadSelectedPlane()
    {
        index = PlayerPrefs.GetInt(SelectedPlane, 0);

        SelectPlane(index);

        GameObject planeInstance = container.InstantiatePrefab(selectedPlane.planeModel);

        cameraManager.OnPlaneInstanceReady(planeInstance);

        var notifier = GetComponent<PrefabInitializationNotifier>();
        StartCoroutine(NotifyAfterDelay(notifier));
    }

    private IEnumerator NotifyAfterDelay(PrefabInitializationNotifier notifier)
    {
        yield return new WaitForSeconds(1f);
        notifier.NotifyPrefabInitialized();
    }
}