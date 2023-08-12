using UnityEngine;

public class PropellerRotate : MonoBehaviour, IAccelerationObserver
{
    private Animator anim;
    private void Awake() => anim = GetComponent<Animator>();

    public void GetAccelerationLevel(float accelerationLevel) => anim.speed = accelerationLevel;
}