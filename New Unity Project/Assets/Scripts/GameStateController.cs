using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    //public PlayerMovement playerMovement;
    //public LevelObjectManager levelObjectManager;

    private enum GameState
    {
        Idle,
        Running,
        GameOver
    }

    private static  GameState _currentState;

    public static bool IsGameRunning => _currentState == GameState.Running;

    public static Action OnGameRestart;

    void Start()
    {
        SetGameState(GameState.Idle);
    }

    private void OnEnable()
    {
        InputController.OnMouseButtonPressed += OnMouseButtonPressed;
        PlayerMovement.OnPlayerCrashed += OnPlayerCrashed;
    }

    private void OnMouseButtonPressed()
    {
        if (_currentState == GameState.Idle)
        {
            SetGameState(GameState.Running);
        }
        if (_currentState == GameState.GameOver)
        {
            RestartGame();
        }
    }

    private void OnPlayerCrashed()
    {
        SetGameState(GameState.GameOver);
    }

    private void OnDisable()
    {
        InputController.OnMouseButtonPressed -= OnMouseButtonPressed;
        PlayerMovement.OnPlayerCrashed -= OnPlayerCrashed;
    }

    private void SetGameState(GameState state) 
    { 
        Debug.Log("Hangi State:" +state);

        _currentState = state;

        switch (_currentState) 
        {
            case GameState.Idle:
                ReadyToRun();
                break;
            case GameState.Running:
                StartGame();
                break;
            case GameState.GameOver:
                GameOver();
                break;
            default:
                break;
        }
    }
    
     private void ReadyToRun()
     {  
     }

    private void StartGame()
    {
    }

    private void GameOver()
    {       
    }

    private void RestartGame()
    {
        OnGameRestart?.Invoke();
    }

}