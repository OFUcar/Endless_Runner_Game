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
        int numberOfLanes = 5;
        _lanePositions = new float[numberOfLanes];
        float laneXPosition = -(numberOfLanes - 1) / 2f * laneWith;

        for(int i = 0; i < numberOfLanes; i++)
        {
            _lanePositions[i] = laneXPosition + i * laneWith;
        }   

        // �lk spawn zaman� ayarlamk i�in 
        _nextSpawnTime = Time.time + SpawnInterval;
    }

    void Update()
    {
        // �u anda obje tan�mlayacak. belirli zaman aral�klar�nda onun if blo�u
        if (Time.time >= _nextSpawnTime)
        {
            SpawnObject();
            // sonras�nda bu d�ng� tekrar edecek. yani zaman� geldik�e yeni obje-->spawn
            _nextSpawnTime = Time.time + SpawnInterval; // deltatime ile �arpmak laz�m m� ??

        }
    }
    void SpawnObject()
    {
        // Rastgele bir obje se�imimiz var
        GameObject objectsToSpawn_1 = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];

        // random pozisyon belirleme

        int randomLane = Random.Range(0, _lanePositions.Length);
        float spawnXLane = _lanePositions[randomLane];

        Vector3 spawnPosition = new Vector3(
            //Random.Range(-4, 4), // yatayda random pozisyonlama
            spawnXLane,
            playerMovementInheritance.transform.position.y, // y deki yeri korumak i�in
            playerMovementInheritance.transform.position.z + 7  //transform.position.z + Random.Range(15 /*  0  */, 500)  // dikeyde random pozisyonlama
        );
        // Trap objesi gelen sonu�taki yere spawnlama
        Instantiate(objectsToSpawn_1, spawnPosition, Quaternion.identity);
    }
}