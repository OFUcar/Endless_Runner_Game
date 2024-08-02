using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject[] objectsToSpawn;

    public bool isGameRunning = false;

    public int minLane = 0;
    public int maxLane = 4;
    public int startLane = 2;

    public float distanceLane =2f;
    public float moveSpeed = 3f;
    public float lineSwitchSpeed = 1.0f;
    public float startTime;
    public float journeyLength;
    public float collisionPauseDuration = 2f;

    private bool isPause = false;

    public Transform startMarker;
    public Transform endMarker;
    
    public Vector3 targetPosition;

    private void Start()
    {
        StartMovementDirection();
    }

    private void Update()
    {
        if (!isGameRunning && Input.GetMouseButtonDown(0))
        {
            isGameRunning = true;
        }

        if (!isGameRunning) 
        {
            return;
        }

        MovePlayerForward();
        ManuelLaneChange();
    }

    private void StartMovementDirection()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(startMarker.position, endMarker.position);
        targetPosition = transform.position;
    }

    private void MovePlayerForward()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }

    private void ManuelLaneChange()
    {
        if (Input.GetKeyDown(KeyCode.A) && startLane > minLane)
        {
            startLane--;
        }

        if (Input.GetKeyDown(KeyCode.D) && startLane < maxLane)
        {
            startLane++;
        }

        targetPosition = new Vector3(startLane * distanceLane - (distanceLane * 2), transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, lineSwitchSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(CheckDead());
        }
    }
    /*private IEnumerator CheckDead()
    {
        isPause = true;
        yield return new WaitForSeconds(collisionPauseDuration);
        isPause = false;
    }*/

    private IEnumerator CheckDead()
    {
        if (!isPause)
        {
            isPause = true;
            DeadGame();
            yield return new WaitForSeconds(collisionPauseDuration);
            ResetGame();

            isPause = false;
        }
    }

    private void DeadGame()
    {
        moveSpeed = 0f;
        lineSwitchSpeed = 0f;

        foreach( var objectsToSpawn_1 in objectsToSpawn)
        {
            objectsToSpawn_1.SetActive(false);
        }

    }

    private void ResetGame()
    {
        transform.position = new Vector3(0, 1,0);
        transform.rotation = Quaternion.identity;
    }
}

// karakter hızı =0 , şerit değştirme  = 0, , obje oluşması duracak
