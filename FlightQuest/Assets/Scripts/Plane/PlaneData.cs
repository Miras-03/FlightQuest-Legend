using UnityEngine;

[CreateAssetMenu(fileName = "PlaneData", menuName = "DataContainer/PlaneData")]
public sealed class PlaneData : ScriptableObject
{
    [HideInInspector] public const float smoothSpeed = 1.4f;

    [HideInInspector] public const float yawAmount = 60f;
    [HideInInspector] public const float pitchAmount = 70f;
    [HideInInspector] public const float rollAmount = 90f;
}
