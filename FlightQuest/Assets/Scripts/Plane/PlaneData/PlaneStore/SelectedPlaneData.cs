using UnityEngine;

[CreateAssetMenu(fileName = "SelectedPlaneData", menuName = "ScriptableObject/SelectedPlaneData")]
public sealed class SelectedPlaneData : ScriptableObject
{
    [HideInInspector] public GameObject planeModel;

    [Space(20)]
    [Header("PlaneStats")]
    public int speed;
    public int acceleration;

    [Space(20)]
    [Header("PlaneSetting")]
    public float yawAmount;
    public float pitchAmount;
    public float pitchGroundAmount;
    public float rollAmount;
}
