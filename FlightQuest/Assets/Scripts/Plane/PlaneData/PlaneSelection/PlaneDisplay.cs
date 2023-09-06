using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject.Asteroids;

public sealed class PlaneDisplay : MonoBehaviour
{
    [Header("PlaneDescription")]
    [SerializeField] private TextMeshProUGUI price;

    [Space(20)]
    [Header("Images")]
    [SerializeField] private Image crystalImage;

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
        crystalImage.enabled = false;
        if (!plane.gamePurchase)
            price.text = $"{plane.price}$";
        else
        {
            price.text = $"{plane.price}";
            crystalImage.enabled = true;
        }

        speed.value = plane.speed;
        acceleration.value = plane.acceleration;
        handling.value = plane.handling;

        if (planeTransform.childCount > 0)
            Destroy(planeTransform.GetChild(0).gameObject);

        Instantiate(plane.planeModel, plane.planeModel.transform.position, plane.planeModel.transform.rotation, planeTransform);

        changeCameraTransform.ChangeTransform(plane.uniqueCode);
    }
}
