using UnityEngine;

public class PropellerRotate : MonoBehaviour, IAccelerationObserver
{
    private Animator anim;
    private const int divideNumber = 5;

    private void Awake() => anim = GetComponent<Animator>();

    public void GetAccelerationLevel(float accelerationLevel)
    {
        accelerationLevel = Mathf.Sqrt(accelerationLevel) / divideNumber;
        Debug.Log(accelerationLevel);
        anim.speed = accelerationLevel;
    }
}