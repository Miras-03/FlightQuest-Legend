using System.Data;
using UnityEngine;

public sealed class CanisterSpawner : MonoBehaviour, IFinishable
{
    [SerializeField] private GameObject canisterPrefab;

    private int canisterCount; 
    private int distance;

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
            Instantiate(canisterPrefab, randomPosition, Quaternion.identity);
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