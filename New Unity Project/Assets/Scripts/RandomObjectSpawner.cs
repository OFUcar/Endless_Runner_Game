using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public PlayerMovement playerMovementInheritance;

    public GameObject[] objectsToSpawn;
    public float SpawnInterval = 3f;
    public float verticalRange = 0f;
    public float laneWith = 2f;

    private float _nextSpawnTime;
    private float[] _lanePositions;
    void Start()
    {
        InitializedLanes(); 
        ScheduleSpawnTime();
    }

    void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            SpawnObject();
        }
    }
    void SpawnObject()
    {
        GameObject objectsToSpawn_1 = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        Vector3 spawnPosition = new Vector3(
            CreateObjectAndSpawn(),
            PlayerMovementY(),
            PlayerMovementZ()
        );
        Instantiate(objectsToSpawn_1, spawnPosition, Quaternion.identity);
    }

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
    private float PlayerMovementY()
    {
        return playerMovementInheritance.transform.position.y;
    }
    private float PlayerMovementZ()
    {
        return playerMovementInheritance.transform.position.z + 7;
    }

    private float CreateObjectAndSpawn()
    {
        int randomLane = Random.Range(0, _lanePositions.Length);
        float spawnXLane = _lanePositions[randomLane];
        return spawnXLane;
    }
}