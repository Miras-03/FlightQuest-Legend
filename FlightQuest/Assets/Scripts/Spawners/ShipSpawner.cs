using UnityEngine;

public sealed class ShipSpawner : MonoBehaviour, IFinishable
{
    [SerializeField] private GameObject prefabToSpawn;

    private int shipCount = 5;
    private int distance = 4000;

    private const int minX = -500;
    private const int maxX = 500;
    private const int yCoordinate = 0;
    private int zCoordinate = 0;

    private const int finishCount = 16;
    private const int defaultShipCount = 5;
    private const int defaultDistance = 2000;
    private const int startLevel = 1;

    private const string ShipCount = nameof(ShipCount);
    private const string ShipDistance = nameof(ShipDistance);
    private const string CurrentLevel = nameof(CurrentLevel);

    private Vector3 randomPosition;

    private void Start()
    {
        GetPreviousCount();

        randomPosition = new Vector3 (0f, yCoordinate, transform.position.z);
        for (int i = 0; i < shipCount; i++)
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

    public void ExecuteFinish() => SaveLevelState();

    private void SaveLevelState()
    {
        int currentLevel = PlayerPrefs.GetInt(CurrentLevel, startLevel);

        if (currentLevel % finishCount == 0)
        {
            GetPreviousCount();

            shipCount++;
            distance += 800;

            PlayerPrefs.SetInt(ShipCount, shipCount);
            PlayerPrefs.SetInt(ShipDistance, distance);
        }
    }

    private void GetPreviousCount()
    {
        shipCount = PlayerPrefs.GetInt(ShipCount, defaultShipCount);
        distance = PlayerPrefs.GetInt(ShipDistance, defaultDistance);
    }
}