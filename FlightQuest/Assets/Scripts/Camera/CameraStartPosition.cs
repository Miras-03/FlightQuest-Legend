using UnityEngine;

public class CameraStartPosition : MonoBehaviour
{
    private PlaneData planeData;

    private Vector3 locationOffset = new Vector3(128, 227, -471);
    private Vector3 rotationOffset = new Vector3(8, 213, 359);

    private Vector3 farLocationOffset = new Vector3(228, 276, -272);
    private Vector3 farRotationOffset = new Vector3(10, 211, 359);

    public void SetTheCamera(PlaneData planeData)
    {
        this.planeData = planeData;

        if (planeData.uniqueCode != 0)
        {
            locationOffset = farLocationOffset;
            rotationOffset = farRotationOffset;
        }

        transform.position = locationOffset;

        Quaternion rotation = Quaternion.Euler(rotationOffset);
        transform.rotation = rotation;
    }
}
