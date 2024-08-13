using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool isGameRunning = false;

    private int _currentLane = 2;

    public float moveSpeed = 3f;
    public float lineSwitchSpeed = 1.0f;
    public float startTime;
    public float journeyLength;
    public float collisionPauseDuration = 2f;

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
        if (Input.GetKeyDown(KeyCode.A) && _currentLane > GameSettings.MinLane)
        {
            _currentLane--;
        }

        if (Input.GetKeyDown(KeyCode.D) && _currentLane < GameSettings.MaxLane)
        {
            _currentLane++;
        }

        targetPosition = new Vector3(GameSettings.StartingLaneXPosition + _currentLane * GameSettings.XDistanceBetweenLanes, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, lineSwitchSpeed * Time.deltaTime);
    }
}
