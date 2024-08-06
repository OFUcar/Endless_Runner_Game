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

    //private bool isPause = false;

    public Transform startMarker;
    public Transform endMarker;
    
    public Vector3 targetPosition;

    private GameStateController gameStateController;

    private void Start()
    {
        gameStateController = FindObjectOfType<GameStateController>();
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

    private void ResetPlayerGame()
    {
        // Burada Gameover olduyğu zmands olasn resetleme işi gerçekleştirilecek

    }
}

// şimdi engeller siliniyor, başa dönüyor ama oyun geri çalışmıyor. Game State Control mekanizması kurmak lazım. 
