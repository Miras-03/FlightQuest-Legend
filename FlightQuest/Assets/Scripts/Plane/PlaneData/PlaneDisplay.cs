using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaneDisplay : MonoBehaviour
{
    [Header("PlaneDescription")]
    [SerializeField] private TextMeshProUGUI price;

    [Space(20)]
    [Header("PlaneStats")]
    [SerializeField] private Slider speed;
    [SerializeField] private Slider acceleration;
    [SerializeField] private Slider handling;

    [Space(20)]
    [Header("PlaneTransform")]
    [SerializeField] private Transform planeTransform;

    [Space(20)]
    [SerializeField] private ChangeCameraTransform changeCameraTransform;

    public void DisplayPlane(PlaneData plane)
    {
        price.text = $"{plane.price}$";

        speed.value = plane.speed;
        acceleration.value = plane.acceleration;
        handling.value = plane.handling;

        if (planeTransform.childCount > 0)
            Destroy(planeTransform.GetChild(0).gameObject);

        Instantiate(plane.planeModel, plane.planeModel.transform.position, plane.planeModel.transform.rotation, planeTransform);

        changeCameraTransform.ChangeTransform(plane.uniqueCode);
    }
}
