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

    public static Action OnObstaclesReset;

    private void Start()
    {
        SpawnObstacles();
    }

    private void Update()
    {
        CarryObstacles();
    }

    private void OnEnable()
    {
        GameStateController.OnGameRestart += OnGameRestartTry;
    }

    private void OnDisable() 
    {
        GameStateController.OnGameRestart += OnGameRestartTry;
    }

    private void OnGameRestartTry()
    {
        float currentSpawnZPosition = 10f;
        foreach (GameObject obstacless in _spawnedObstacles)
        {
            obstacless.transform.position = new Vector3(GetRandomXPosition(), 0.5f, currentSpawnZPosition);
            currentSpawnZPosition += GameSettings.ObstacleZDifference;
        }
    }

    private void SpawnObstacles()
    {
        _spawnedObstacles = new List<GameObject>();
        
        float currentSpawnZPosition = GameSettings.ObstacleStartingZPosition;

        while (currentSpawnZPosition < GameSettings.ObstacleMaxSpawnedDistanceAccordingToPlayer)
        {
            GameObject spawnedObject = Instantiate(_obstaclePrefab);
            spawnedObject.transform.position = new Vector3(GetRandomXPosition(), 0.5f, currentSpawnZPosition);
            _spawnedObstacles.Add(spawnedObject);
            currentSpawnZPosition += GameSettings.ObstacleZDifference;
        }
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