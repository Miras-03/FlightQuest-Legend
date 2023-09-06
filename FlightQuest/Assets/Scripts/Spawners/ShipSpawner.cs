using UnityEngine;

public sealed class ShipSpawner : MonoBehaviour, IFinishable
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private GameObject airportPlace;

    private int distance;
    private const int shipCount = 8;

    private const int minX = -500;
    private const int maxX = 500;
    private const int yCoordinate = 0;
    private int zCoordinate = 0;

    private const int defaultDistance = 4000;

    private const string ShipDistance = nameof(ShipDistance);
    private const string CurrentLevel = nameof(CurrentLevel);

    private Vector3 randomPosition;

    private void Start()
    {
        PlayerPrefs.DeleteKey("ShipCount");
        GetPreviousCount();

        randomPosition = new Vector3 (0f, yCoordinate, transform.position.z);
        for (int i = 0; i < shipCount; i++)
        {
            Quaternion randomRotation = Random.rotation;
            randomRotation.eulerAngles = new Vector3(0f, randomRotation.eulerAngles.y, 0f);
            Instantiate(prefabToSpawn, randomPosition, randomRotation);
            randomPosition = GenerateRandomPosition();
        }

        float airPortDistance = randomPosition.z - distance/2;
        Vector3 airportPosition = new Vector3(0f, 0f, airPortDistance);
        airportPlace.transform.position = airportPosition;
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

    public void ExecuteFinish() => SaveLevelState();

    private void SaveLevelState()
    {
        GetPreviousCount();
        distance += 50;

        PlayerPrefs.SetInt(ShipDistance, distance);
    }

    private void GetPreviousCount() => distance = PlayerPrefs.GetInt(ShipDistance, defaultDistance);
}