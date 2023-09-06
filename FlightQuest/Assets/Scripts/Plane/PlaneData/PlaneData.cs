using UnityEngine;

[CreateAssetMenu(fileName = "PlaneData", menuName = "ScriptableObject/PlaneData")]
public sealed class PlaneData : ScriptableObject
{
    public GameObject planeModel;

    [Header("PlaneDescription")]
    public int price;
    public int uniqueCode;
    public bool gamePurchase;

    [Space(20)]
    [Header("PlaneStats")]
    public int speed;
    public int acceleration;
    public int deceleration;
    public int handling;

    [Space(20)]
    [Header("PlaneSetting")]
    public int yawAmount;
    public int pitchAmount;
    public int groundPitchAmount = 0;
    public int rollAmount;

    public float rotationSmoothSpeed;
}
