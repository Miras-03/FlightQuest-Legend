using UnityEngine;

public class ShipSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    private const int numberOfPrefabsToSpawn = 5;
    private const float distance = 4000f;

    private const float minX = -500f;
    private const float maxX = 500f;
    private const float yCoordinate = 0f;
    private float zCoordinate = 0f;

    private Vector3 randomPosition;

    private void Start()
    {
        randomPosition = new Vector3 (0f, yCoordinate, transform.position.z);
        for (int i = 0; i < numberOfPrefabsToSpawn; i++)
        {
            Quaternion randomRotation = Random.rotation;
            randomRotation.eulerAngles = new Vector3(0f, randomRotation.eulerAngles.y, 0f);
            Instantiate(prefabToSpawn, randomPosition, randomRotation);
            randomPosition = GenerateRandomPosition();
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(minX, maxX),
            yCoordinate,
            zCoordinate += distance
        );
        return randomPosition;
    }
}