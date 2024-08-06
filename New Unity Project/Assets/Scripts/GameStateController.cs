using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public RandomObjectSpawner randomObjectSpawner;

    public enum GameState
    {
        Idle,
        Running,
        Pause,
        GameOver
    }

    public GameState currentState;

    void Start()
    {
        SetGameState(GameState.Idle);
    }

    void Update()
    {
        if (currentState == GameState.Idle && Input.GetMouseButtonDown(0))
        {
            SetGameState(GameState.Running);
        }
    }

    private void SetGameState(GameState state) 
    { 
        currentState = state;

        switch (currentState) 
        {
            case GameState.Idle:
                OnGameIdle();
                break;
            case GameState.Running:
                OnGameRunning();
                break;
            case GameState.Pause:
                OnGamePause();
                break;
            case GameState.GameOver:
                OnGameOver();
                break;
            default:
                break;
        }
    }
    
     private void OnGameIdle()
    {
        playerMovement.isGameRunning = false;
        playerMovement.moveSpeed = 0f;
        randomObjectSpawner.enabled = false;
    }

    private void OnGameRunning()
    {
        playerMovement.isGameRunning = true;
        playerMovement.moveSpeed = 3f;
        randomObjectSpawner.enabled = true;
    }

    private void OnGamePause()
    {
        playerMovement.isGameRunning = false;
        playerMovement.moveSpeed = 0f;
        randomObjectSpawner.enabled = false;
    }

    private void OnGameOver()
    {
        playerMovement.isGameRunning = false;
        playerMovement.moveSpeed = 0f;
        randomObjectSpawner.enabled = false;
        ResetGame();
    }

    public void PauseGame()
    {
        SetGameState(GameState.Pause);
    }

    public void ResumeGame()
    {
        SetGameState(GameState.Running);
    }

    public void EndGame()
    {
        SetGameState(GameState.GameOver);
    }
    // Buradan sonraki olan fonksiyonlarý taþýman gerekiyor. Mesela ClearGame Gibi 








    public void DeadGame()
    {
        foreach (var objectsToSpawn_1 in playerMovement.objectsToSpawn)
        {
            objectsToSpawn_1.SetActive(false);
        }
    }

    public void ResetGame()
    {
        // Burasý için C# event leri kullanman gerek. SS ler aldým zaten  ClearGame olmayacak onun yerine Object Pooling yöntemini kullan


        playerMovement.transform.position = new Vector3(0,1,0);
        playerMovement.transform.rotation = Quaternion.identity;
        ClearGame();
        SetGameState(GameState.Idle);
    }

    public void ClearGame()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach(GameObject obstacle in obstacles)
        {
            Destroy(obstacle);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag("Obstacle"))
        {
            StartCoroutine(CheckDead());
        }
    }

    private IEnumerator CheckDead()
    {
        bool isPause = false;

        if (!isPause)
        {
            isPause = true;
            DeadGame();
            yield return new WaitForSeconds(playerMovement.collisionPauseDuration);
            ResetGame();
            isPause = false;
        }
    }
}

// Þu anda ObstacleS Tag ini yinr baðlayamadým. Ama game State Controllerýn Çalýþmasý gerekiyor yine de

