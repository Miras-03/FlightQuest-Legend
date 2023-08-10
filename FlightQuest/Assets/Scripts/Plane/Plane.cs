using UnityEngine;

public abstract class Plane : MonoBehaviour
{
    public float maxSpeed;
    public float currentSpeed = 0f;

    [HideInInspector] public float lowMaxSpeed;
    [HideInInspector] public float mediumMaxSpeed;
    [HideInInspector] public float highMaxSpeed;

    [HideInInspector] public float lowAcceleration;
    [HideInInspector] public float mediumAcceleration;
    [HideInInspector] public float highAcceleration;

    [HideInInspector] public float currentAcceleration;
    [HideInInspector] public int accelerationCount = -1;

    [HideInInspector] public bool isLandingGearRemoved = true;
    [HideInInspector] public bool isBurned = false;

    protected Rigidbody rb;

    public abstract void Move();
}