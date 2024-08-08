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
    //private float _laneWith = 2f;

    private List<GameObject> _spawnedObstacles;

    private void Start()
    {
        _spawnedObstacles = new List<GameObject>();

        float currentSpawnZPosition = _startingZPosition;

        while(currentSpawnZPosition < _maxSpawnedDistanceAccordingToPlayer)
        {
            GameObject spawnedObject = Instantiate(_obstaclePrefab, new Vector3(0, 0.5f, currentSpawnZPosition), Quaternion.identity);
            _spawnedObstacles.Add(spawnedObject);

            currentSpawnZPosition += _zDifference;
        }
    }
    //Burada Lane lerin ortasýnda ve random  x deðeri almak zorunda , üst üste de binmeyecek yani oluþan objeler arasýnda en az 1.0f mesafe olmasý gerekiyor.

    private void RandomObstaclesPosition()
    {
       foreach(var obstacle in _spawnedObstacles)
        {
            float randomX = Random.Range(_minLane, _maxLane);

            float randomZOffset = Random.Range(_maxSpawnedDistanceAccordingToPlayer / 2, _maxSpawnedDistanceAccordingToPlayer / 2);
            float newZPosition = CurrentPlayerZPosition() + randomZOffset;
            _ = CurrentPlayerZPosition() + randomZOffset;

            Vector3 newPosition = new Vector3(randomX, 0.5f, newZPosition);

            obstacle.transform.position = newPosition;
        }
    }

    // burada ise player karakteri ilk engeli geçti mi diye kontrol et, eðer geçtiyse kameranýn 2f gerisinde kalýnca listeye geri al ve ilerideki bir pozisyona random bir x pozisyonuna taþý

    private void Update()
    {
        float playerZPosition =  CurrentPlayerZPosition();
        GameObject firstObstacle = _spawnedObstacles[0];

        foreach (var obstacle in _spawnedObstacles)
        {
            if (obstacle.transform.position.z < playerZPosition - 3f)
            {
                float randomX = Random.Range(_minLane, _maxLane); // x-de random
                // z de random ama playerdan 200 birim ötede(_max...)
                float newZPosition = playerZPosition + _maxSpawnedDistanceAccordingToPlayer;
                Vector3 newPosition = new Vector3(randomX, 0.5f, newZPosition);

                firstObstacle.transform.position = newPosition;
            }
        }
    }

    private float CurrentPlayerZPosition()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        return player.transform.position.z;
    }
    /* ortalamaya gerek yok 
    private void AverageLanes()
    {
        int numberOfLanes = 5;
        _lanePosition = new float[numberOfLanes];
        float laneXPosition = -(numberOfLanes -1) / 2f * _laneWith;
    }
      */
}
