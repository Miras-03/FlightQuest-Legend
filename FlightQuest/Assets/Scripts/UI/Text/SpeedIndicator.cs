using PlaneSection;
using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class SpeedIndicator : MonoBehaviour
{
    private AirPlane plane;
    private TextMeshProUGUI speed;

    [Inject]
    public void Construct(AirPlane plane, TextMeshProUGUI[] texts)
    {
        this.plane = plane;
        speed = texts[1];
    }

    private void Start() => StartCoroutine(ChangeSpeed());

    private void IndicateSpeed() => speed.text = $"{(int)plane.currentSpeed} km/h";

    private IEnumerator ChangeSpeed()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            IndicateSpeed();
        }
    }
}
