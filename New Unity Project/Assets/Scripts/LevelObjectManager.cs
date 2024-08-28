using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelObjectManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _obstaclePrefab;
    [SerializeField]
    private GameObject _coinPrefab;
    [SerializeField]
    private GameObject[] _groundPrefabs;

    private int _nearestObstacleIndex = 0;
    private int _nearestCoinIndex = 0;
    private int _nearestGroundIndex = 0;

    private List<GameObject> _spawnedCoins;
    private List<GameObject> _spawnedObstacles;

    private PlayerMovement _playerMovement;

    public static Action ResetObstacles;

    public static Action OnObstaclesReset;

    private void Start()
    {
        SpawnGrounds();
        SpawnObstacles();
        SpawnCoins();
        _playerMovement = FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        CarryObstacles();
        CarryCoins();
        CarryGrounds();
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
        _nearestObstacleIndex = 0;
        
        currentSpawnZPosition = 5f;
        foreach (GameObject coinss in _spawnedCoins)
        {
            coinss.transform.position = new Vector3(GetRandomXPosition(), 0.75f, currentSpawnZPosition);
            currentSpawnZPosition += GameSettings.CoinZDifference;
        }
        _nearestCoinIndex = 0;

        if (_playerMovement != null) 
        {
            _playerMovement.ResetCollectedCoins();
        }
        else
        {
            Debug.Log("Player Movement'dan hata mesajý geliyor");
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

    private void SpawnCoins()
    {
        _spawnedCoins = new List<GameObject>();

        float currentSpawnZPosition = GameSettings.CoinStartingZPosition;
        while(currentSpawnZPosition < GameSettings.CoinMaxSpawnedDistanceAccordingToPlayer)
        {
            GameObject spawnedCoin = Instantiate(_coinPrefab);
            spawnedCoin.transform.position = new Vector3(GetRandomXPosition(), 0.75f, currentSpawnZPosition);
            spawnedCoin.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            spawnedCoin.transform.localScale = new Vector3(1f, 0.05f,1f);
            _spawnedCoins.Add(spawnedCoin);
            currentSpawnZPosition += GameSettings.CoinZDifference;
        } 
    }

    private void CarryCoins()
    {
        float playerZPosition = CurrentPlayerZPosition();

        GameObject nearestCoin = _spawnedCoins[_nearestCoinIndex];

        if (nearestCoin.transform.position.z < playerZPosition -3f)
        {
            float newZPosition = _spawnedCoins[_nearestCoinIndex].transform.position.z + GameSettings.CoinMaxSpawnedDistanceAccordingToPlayer;
            Vector3 newPosition = new Vector3(GetRandomXPosition(), 0.75f, newZPosition);

            nearestCoin.transform.position = newPosition;

            nearestCoin.SetActive(true);

            _nearestCoinIndex++;

            if (_nearestCoinIndex >= _spawnedCoins.Count) 
            {
                _nearestCoinIndex = 0;
            }
        }
    }

    private void SpawnGrounds()
    {
        float startGroundZPosition = 100f;

        for (int i = 0; i < _groundPrefabs.Length; i++)
        {
            GameObject ground = Instantiate(_groundPrefabs[i]);
            ground.transform.position = new Vector3(0, 0, startGroundZPosition);
            ground.transform.localScale = new Vector3(1, 1, 50);
            _groundPrefabs[i] = ground;
            startGroundZPosition += ground.transform.localScale.z * 2;
        }
    }
    private void CarryGrounds()
    {
        float playerZPosition = CurrentPlayerZPosition();
        float groundLength = 10f;
        float offset = 40f;

        GameObject nearestGround = _groundPrefabs[_nearestGroundIndex];

        if (nearestGround.transform.position.z + groundLength + offset < playerZPosition)
        {
            float farthestZPosition = float.MinValue;
            for (int i = 0; i < _groundPrefabs.Length; i++)
            {
                if (_groundPrefabs[i].transform.position.z > farthestZPosition)
                {
                    farthestZPosition = _groundPrefabs[i].transform.position.z;
                }
            }

            nearestGround.transform.position = new Vector3(0, 0, farthestZPosition + groundLength + offset);

            _nearestGroundIndex++;
            if (_nearestGroundIndex >= _groundPrefabs.Length)
            {
                _nearestGroundIndex = 0;
            }
        }
    }

}