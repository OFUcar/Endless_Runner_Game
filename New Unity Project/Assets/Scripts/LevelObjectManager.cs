using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelObjectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _obstaclePrefab;

    private int _nearestObstacleIndex = 0;

    private List<GameObject> _spawnedObstacles;

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

    private float GetRandomXPosition()
    {
        int randomLane = Random.Range(GameSettings.MinLane, GameSettings.MaxLane +1 );
        //Debug.Log()
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