using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private int _currentLane = 2;
    private int _collectedCoins = 0;

    private const float MOVE_SPEED = 3f;
    private const float LaneSwitchSpeed = 30f;
        
    public Vector3 targetPosition;

    public static Action OnPlayerCrashed;
    public static Action OnCoinCollected;

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
        OnCoinCollected += HandleCoinCollected;
    }

    private void OnGameRestart()
    {
        transform.position = StartingPosition;
        SetPlayerToStart(); 
        _currentLane = 2;
    }

    private void HandleCoinCollected()
    {
        _collectedCoins++;
        Debug.Log("Toplanan Coin sayýsý:" +_collectedCoins);
    }
     
    private void OnDirectionButtonPressed(int direction)
    {
        _currentLane =Mathf.Clamp(_currentLane+direction, GameSettings.MinLane, GameSettings.MaxLane);
    }

    private void OnDisable()
    {
        InputController.OnDirectionButtonPressed -= OnDirectionButtonPressed;
        GameStateController.OnGameRestart -= OnGameRestart;
        OnCoinCollected -= HandleCoinCollected;
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
        transform.position = StartingPosition;
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
        if(other.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            OnCoinCollected?.Invoke();
        }
    }
    public void ResetCollectedCoins()
    {
        _collectedCoins = 0;
        Debug.Log("Coin Sayýsý Resetlendi!!!!");
    }

}
