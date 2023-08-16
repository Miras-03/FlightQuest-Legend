using UnityEngine;

[CreateAssetMenu(fileName = "LightPlane", menuName = "DataContainer/PlaneData/LightPlane")]
public sealed class PlaneData : ScriptableObject
{
    public const float smoothAirSpeed = 1.4f;
    public const float smoothLandSpeed = 0.9f;

    public const float yawAmount = 50f;
    public const float pitchAmount = 50f;
    public const float pitchGroundAmount = 20f;
    public const float rollAmount = 90f;

    public const float rotationSmoothSpeed = 1.4f;
}
