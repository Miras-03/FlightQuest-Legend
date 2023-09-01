using UnityEngine;

public sealed class CanisterSpawner : MonoBehaviour, IFinishable
{
    [SerializeField] private GameObject prefabToSpawn;
    [SerializeField] private GameObject airportPlace;
    public int canisterCount; 
    public int distance;

    private const int minX = -300;
    private const int maxX = 300;
    private const int minY = 100;
    private const int maxY = 500;

    private const int defaultCanisterCount = 10;
    private const int defaultDistance = 2000;

    private float zCoordinate = 0f;

    private const string CanisterCount = nameof(CanisterCount);
    private const string CanisterDistance = nameof(CanisterDistance);

    private Vector3 randomPosition;

    private void Start()
    {
        LoadPreviousState();
        SetPositions();
    }

    private void SetPositions()
    {
        randomPosition = transform.position;
        for (int i = 0; i < canisterCount; i++)
        {
            Instantiate(prefabToSpawn, randomPosition, Quaternion.identity);
            randomPosition = GenerateRandomPosition();
        }

        float airPortDistance = randomPosition.z + distance;
        Vector3 airportPosition = new Vector3(0f, 0f, airPortDistance);
        airportPlace.transform.position = airportPosition;
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

    public void ExecuteFinish() => SaveLevelState();

    private void SaveLevelState()
    {
        LoadPreviousState();

        canisterCount++;
        distance += 50;

        PlayerPrefs.SetInt(CanisterCount, canisterCount);
        PlayerPrefs.SetInt(CanisterDistance, distance);
    }

    private void LoadPreviousState()
    {
        canisterCount = PlayerPrefs.GetInt(CanisterCount, defaultCanisterCount);
        distance = PlayerPrefs.GetInt(CanisterDistance, defaultDistance);
    }
}