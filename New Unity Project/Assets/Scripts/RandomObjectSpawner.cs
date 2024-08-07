using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectSpawner : MonoBehaviour
{
    public PlayerMovement playerMovement;

    public GameObject[] objectsToSpawn;

    public int poolSize = 300;

    public float SpawnInterval = 3f;
    public float verticalRange = 0f;
    public float laneWith = 2f;
    private float _nextSpawnTime;
    private float[] _lanePositions;


    private Dictionary<GameObject, ObjectPooling> _objectPools;

    private List<GameObject> _spawnedObjects;

    void Start()
    {
        InitializedLanes();
        //InitializeObjectPools();
        InitializeSpawnedObjects();
        ScheduleSpawnTime();
    }

    void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            SpawnObject();
            ScheduleSpawnTime();
        }

        RepositionObjects();
    }

    private void RepositionObjects()
    {
        _objectPools = new Dictionary<GameObject, ObjectPooling>();

        Debug.Log("Object Pools atama yapýldý");

        foreach (var prefab in objectsToSpawn)
        {
            GameObject poolObject = new GameObject(prefab.name + "Pool");
            poolObject.transform.parent = transform; 
            ObjectPooling pool = poolObject.AddComponent<ObjectPooling>();
            pool.objectPrefab = prefab;
            pool.poolSize = poolSize;
            _objectPools.Add(prefab, pool);
        }
    }

    private void InitializeSpawnedObjects()
    {
        _spawnedObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            SpawnObject(true);
        }
    }

    private void SpawnObject(bool initialSpawn = false)
    {
        GameObject prefab = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        Debug.Log("dictinory null = "+ _objectPools == null);
        Debug.Log("dictionary de key e sahip value var mý = " +_objectPools.ContainsKey(prefab).ToString());
        GameObject objectToSpawn = _objectPools[prefab].GetPooledObject();

        Vector3 playerPosition = playerMovement.transform.position;
        Vector3 spawnPosition = new Vector3(
            GetRandomLanePositionX(),
            playerPosition.y,
            playerPosition.z + (initialSpawn ? 200 : 7)
            );

        objectToSpawn.transform.position = spawnPosition;
        objectToSpawn.transform.rotation = Quaternion.identity;
        objectToSpawn.SetActive(true);
        _spawnedObjects.Add(objectToSpawn);
    }

    private void RepositionObject()
    {
        Vector3 cameraPosition = Camera.main.transform.position;
        float repositionZ = cameraPosition.z = 6f;

        foreach (var poolGameObject in _spawnedObjects)
        {
            if (poolGameObject.activeInHierarchy && poolGameObject.transform.position.z <repositionZ)
            {
                _objectPools[poolGameObject].ReturnObjectToPool(poolGameObject);
                Vector3 newPosition = new Vector3(
                    GetRandomLanePositionX(),
                    playerMovement.transform.position.y,
                    playerMovement.transform.position.z + 210
                    );
                poolGameObject.transform.position = newPosition;
                poolGameObject.SetActive(true);
            }
        }
    }
    // Bu Random Obje oluþturma sistemini tamamen deðiþtirmen gerekiyor.

    private void InitializedLanes()
    {
        int numberOfLanes = 5;
        _lanePositions = new float[numberOfLanes];
        float laneXPosition = -(numberOfLanes - 1) / 2f * laneWith;

        for (int i = 0; i < numberOfLanes; i++)
        {
            _lanePositions[i] = laneXPosition + i * laneWith;
        }
    }

    private void ScheduleSpawnTime()
    {
        _nextSpawnTime = Time.time + SpawnInterval; 
    }

    private float GetRandomLanePositionX()
    {
        int randomLane = Random.Range(0, _lanePositions.Length);
        float spawnXLane = _lanePositions[randomLane];
        return spawnXLane;
    }

    private void ResetGame()
    {

    }
}


/*
 void SpawnObject() // burasý komple deðiþecek 
    {
        GameObject objectsToSpawn_1 = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        Vector3 playerPosition = playerMovement.transform.position;
        Vector3 spawnPosition = new Vector3(
            GetRandomLanePositionX(),
            playerPosition.y,
            playerPosition.z + 7
        );

        Instantiate(objectsToSpawn_1, spawnPosition, Quaternion.identity); // en kötü yöntemlreden ----  ObjectPooling yöntemini kullan ,instantiate çöp
    }
 */