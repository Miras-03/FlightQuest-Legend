using UnityEngine;

public sealed class PropellerRotate : MonoBehaviour
{
    private Animator anim;
    private const int divideNumber = 5;

    private void Awake() => anim = GetComponent<Animator>();

    public void GetAccelerationLevel(float accelerationLevel)
    {
        accelerationLevel = Mathf.Sqrt(accelerationLevel) / divideNumber;
        anim.speed = accelerationLevel;
    }
}