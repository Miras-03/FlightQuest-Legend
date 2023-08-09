using UnityEngine;

[CreateAssetMenu(fileName = "PlaneData", menuName = "DataContainer/PlaneData")]
public sealed class PlaneData : ScriptableObject
{
    public const float smoothAirSpeed = 1.4f;
    public const float smoothLandSpeed = 0.9f;

    public const float yawAmount = 60f;
    public const float pitchAmount = 70f;
    public const float rollAmount = 90f;
}
