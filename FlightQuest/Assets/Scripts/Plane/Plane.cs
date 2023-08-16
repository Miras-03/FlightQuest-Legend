using UnityEngine;

public abstract class Plane : MonoBehaviour
{
    [HideInInspector] public float maxPossibleSpeed;

    public float maxSpeed;
    public float currentSpeed = 0f;

    [HideInInspector] public int speedAcceleration;
    [HideInInspector] public int decelerationFactor;

    [HideInInspector] public bool isLandingGearRemoved = true;
    [HideInInspector] public bool isBurned = false;

    [HideInInspector] public Rigidbody rb;

    public abstract void Move();
}