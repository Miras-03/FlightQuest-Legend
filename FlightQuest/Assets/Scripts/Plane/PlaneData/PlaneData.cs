using UnityEngine;

[CreateAssetMenu(fileName = "PlaneData", menuName = "ScriptableObject/PlaneData")]
public sealed class PlaneData : ScriptableObject
{
    public GameObject planeModel;

    [Header("PlaneDescription")]
    public int price;
    public int uniqueCode;

    [Space(20)]
    [Header("PlaneStats")]
    public int speed;
    public int acceleration;
    public int handling;

    [Space(20)]
    [Header("PlaneSetting")]
    public float yawAmount;
    public float pitchAmount;
    public float pitchGroundAmount;
    public float rollAmount;

    public float rotationSmoothSpeed;
}
