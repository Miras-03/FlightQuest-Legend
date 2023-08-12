using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;
    private const int numberOfPrefabsToSpawn = 10;
    private const float distance = 2000f;

    private const float minX = -300f;
    private const float maxX = 300f;
    private const float minY = 300f;
    private const float maxY = 500f;
    private float zCoordinate = 0f;

    private Vector3 randomPosition;

    private void Start()
    {
        randomPosition = transform.position;
        for (int i = 0; i < numberOfPrefabsToSpawn; i++)
        {
            Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
            randomPosition = GenerateRandomPosition();
        }
    }

    private Vector3 GenerateRandomPosition()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(minX, maxX),
            Random.Range(minY, maxY),
            zCoordinate += distance
        );
        return randomPosition;
    }
}