using System.Collections;
using UnityEngine;

public class ChangeCameraTransform : MonoBehaviour
{
    private Vector3 downCameraPosition = new Vector3(0f, 62f, -142f);
    private Vector3 downCameraRotation = new Vector3(9f, 0f, 0f);

    private Vector3 upCameraPosition = new Vector3(23f, 90f, -192);
    private Vector3 upCameraRotation = new Vector3(6f, 0, 0);

    public void ChangeTransform(int uniqueCode)
    {
        Vector3 targetPosition;
        Vector3 targetRotation;

        if (uniqueCode == 0)
        {
            targetPosition = downCameraPosition;
            targetRotation = downCameraRotation;
        }
        else
        {
            targetPosition = upCameraPosition;
            targetRotation = upCameraRotation;
        }

        StartCoroutine(SmoothlyTransitionCamera(targetPosition, targetRotation));
    }

    private IEnumerator SmoothlyTransitionCamera(Vector3 targetPosition, Vector3 targetRotation)
    {
        Vector3 initialPosition = transform.position;
        Vector3 initialRotation = transform.rotation.eulerAngles;

        float elapsedTime = 0f;
        const float duration = 3f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            transform.rotation = Quaternion.Euler(Vector3.Lerp(initialRotation, targetRotation, elapsedTime / duration));

            yield return null;
            elapsedTime += Time.fixedDeltaTime;
        }

        transform.position = targetPosition;
        transform.rotation = Quaternion.Euler(targetRotation);
    }
}