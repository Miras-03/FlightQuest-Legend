using System;
using UnityEngine;

public class PlaneChanger : MonoBehaviour
{
    [SerializeField] private ScriptableObject[] planeData;
    [SerializeField] private PlaneDisplay planeDisplay;

    private int currentIndex = 0;

    private void Start() => ChangePlaneData(currentIndex);

    public void ChangePlaneData(int change)
    {
        currentIndex += change;
        if (currentIndex < 0) 
            currentIndex = planeData.Length - 1;
        else if (currentIndex > planeData.Length - 1) 
            currentIndex = 0;

        planeDisplay?.DisplayPlane((PlaneData)planeData[currentIndex]);
    }
}
