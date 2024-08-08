using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _obstaclePrefab;

    private int _poolSize = 300;
    private int _minLane = 0;
    private int _maxLane = 4;

    private float _startingZPosition = 10f;
    private float _zDifference = 2f;
    private float _maxSpawnedDistanceAccordingToPlayer = 200f;
    private float[] _lanePosition;
    private float _laneWith = 2f;

    private List<GameObject> _spawnedObstacles;

    private void Start()
    {
        SpawnedObstacles();
    }

    private void Update()
    {
        CarryingObstacles();
    }

    //Burada objeyi pooldan çekip oluþturdum
    private void SpawnedObstacles()
    {
        _spawnedObstacles = new List<GameObject>();

        float currentSpawnZPosition = _startingZPosition;

        while (currentSpawnZPosition < _maxSpawnedDistanceAccordingToPlayer)
        {
            GameObject spawnedObject = Instantiate(_obstaclePrefab, new Vector3(0, 0.5f, currentSpawnZPosition), Quaternion.identity);
            _spawnedObstacles.Add(spawnedObject);

            //RandomObstaclesPosition();

            currentSpawnZPosition += _zDifference;
        }
    }

    //burada objeyi ileriye taþýdým. ama sadece ilk objeyi
    private void CarryingObstacles()
    {
        float playerZPosition = CurrentPlayerZPosition();
        GameObject firstObstacle = _spawnedObstacles[0];

        if (firstObstacle.transform.position.z < playerZPosition - 3f)
        {
            int randomX = Random.Range(_minLane, _maxLane + 1);
            Debug.Log($"Random X Position: {randomX}");

            float newZPosition = _startingZPosition + _maxSpawnedDistanceAccordingToPlayer;
            Vector3 newPosition = new Vector3(randomX, 0.5f, newZPosition);

            firstObstacle.transform.position = newPosition;
        }
    }

    // Burada player ý tagledim
    private float CurrentPlayerZPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player.transform.position.z;
    }

    //Burada objeleri random olarak pozisyonlara getir
    private void RandomObstaclesPosition()
    {
        foreach (var obstacle in _spawnedObstacles)
        {
            int randomLane = Random.Range(0, 4);
            float randomX = randomLane * 2f;

            float randomZOffset = Random.Range(_maxSpawnedDistanceAccordingToPlayer / 2, _maxSpawnedDistanceAccordingToPlayer / 2);
            float newZPosition = CurrentPlayerZPosition() + randomZOffset;
            _ = CurrentPlayerZPosition() + randomZOffset;

            Vector3 newPosition = new Vector3(randomX, 0.5f, newZPosition);

            InitializedLanes();

            obstacle.transform.position = newPosition;
        }
    }

    // burada objeleri lane ler üzerinde ortaladým
    private void InitializedLanes()
    {
        int numberOfLanes = 5;
        _lanePosition = new float[numberOfLanes];
        float laneXPosition = -(numberOfLanes - 1) / 2f * _laneWith;

        for (int i = 0; i < numberOfLanes; i++)
        {
            _lanePosition[i] = laneXPosition + i * _laneWith;
        }
    }
}