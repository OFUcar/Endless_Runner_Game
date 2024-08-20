using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int _currentLane = 2;

    private const float MOVE_SPEED = 3f;
    private const float LaneSwitchSpeed = 30f;
        
    public Vector3 targetPosition;

    public static Action OnPlayerCrashed;

    private LevelObjectManager levelObjectManager;

    private static readonly Vector3 StartingPosition = new Vector3(0,1,0);

    private void Start()
    {
        SetPlayerToStart();
    }

    private void OnEnable()
    {
        InputController.OnDirectionButtonPressed += OnDirectionButtonPressed;
        GameStateController.OnGameRestart += OnGameRestart;
    }

    private void OnGameRestart()
    {
        transform.position = StartingPosition;
        SetPlayerToStart(); 
        _currentLane = 2;
    }
     
    private void OnDirectionButtonPressed(int direction)
    {
        _currentLane =Mathf.Clamp(_currentLane+direction, GameSettings.MinLane, GameSettings.MaxLane);
    }

    private void OnDisable()
    {
        InputController.OnDirectionButtonPressed -= OnDirectionButtonPressed;
        GameStateController.OnGameRestart -= OnGameRestart;
    }
    private void Update()

    {
        if (!GameStateController.IsGameRunning) 
        {
            return;
        }

        MovePlayerForward();
        MovePlayerSideway();
    }

    private void SetPlayerToStart()
    {
        transform.position = new Vector3(0, 1, 0);
        _currentLane = 2;
        targetPosition = transform.position;
    }

    private void MovePlayerForward()
    {
        transform.position += Vector3.forward * MOVE_SPEED * Time.deltaTime;
    }

    private void MovePlayerSideway()
    {
        targetPosition = new Vector3(GameSettings.StartingLaneXPosition + _currentLane * GameSettings.XDistanceBetweenLanes, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, LaneSwitchSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            OnPlayerCrashed?.Invoke();
        }
    }
}
