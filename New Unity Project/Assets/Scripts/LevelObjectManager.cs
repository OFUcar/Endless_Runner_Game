using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _obstaclePrefab;

    private int _nearestObstacleIndex = 0;

    private List<GameObject> _spawnedObstacles;

    public static Action ResetObstacles;

    private void Start()
    {
        SpawnObstacles();
    }

    private void Update()
    {
        CarryObstacles();
    }

    private void SpawnObstacles()
    {
        _spawnedObstacles = new List<GameObject>();

        float currentSpawnZPosition = GameSettings.ObstacleStartingZPosition;

        while (currentSpawnZPosition < GameSettings.ObstacleMaxSpawnedDistanceAccordingToPlayer)
        {
            GameObject spawnedObject = Instantiate(_obstaclePrefab, new Vector3(GetRandomXPosition(), 0.5f, currentSpawnZPosition), Quaternion.identity);
            _spawnedObstacles.Add(spawnedObject);
            currentSpawnZPosition += GameSettings.ObstacleZDifference;
        }
    }

    public void ResetObstaclesToStartPosition()
    {
        for (int i =0; i<_nearestObstacleIndex; i++ )
        {
            GameObject obstacleToReset = _spawnedObstacles[i];

            float newZPosition = GameSettings.ObstacleStartingZPosition + i * GameSettings.ObstacleZDifference;
            Vector3 newObstaclesPosition = new Vector3(GetRandomXPosition(), 0.5f, newZPosition);

            if (_spawnedObstacles != null)
            {
                Debug.LogError("Spawned Obstacles listesi null!");
            }

            obstacleToReset.transform.position = newObstaclesPosition;
        }

        for (int i = _nearestObstacleIndex; i == 0; i--)
        {
            CarryObstacles();
        }

        _nearestObstacleIndex = 0;
    }

    private float GetRandomXPosition()
    {
        int randomLane = UnityEngine.Random.Range(GameSettings.MinLane, GameSettings.MaxLane + 1);
        return GameSettings.StartingLaneXPosition + randomLane * GameSettings.XDistanceBetweenLanes;
    }

    private void CarryObstacles()
    {
        float playerZPosition = CurrentPlayerZPosition();
        GameObject nearestObstacle = _spawnedObstacles[_nearestObstacleIndex];

        if (nearestObstacle.transform.position.z < playerZPosition - 3f)
        {
            float newZPosition = _spawnedObstacles[_nearestObstacleIndex].transform.position.z + GameSettings.ObstacleMaxSpawnedDistanceAccordingToPlayer;
            Vector3 newPosition = new Vector3(GetRandomXPosition(), 0.5f, newZPosition);

            nearestObstacle.transform.position = newPosition;

            _nearestObstacleIndex++;

            if (_nearestObstacleIndex >= _spawnedObstacles.Count)
            {
                _nearestObstacleIndex = 0;
            }
        }
    }

    private float CurrentPlayerZPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player.transform.position.z;
    }
}