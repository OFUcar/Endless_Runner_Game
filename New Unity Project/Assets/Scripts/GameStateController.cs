using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateController : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public ObjectPooling objectPooling;

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
        ReferenceCheck();
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
        objectPooling.enabled = false;
    }

    private void OnGameRunning()
    {
        playerMovement.isGameRunning = true;
        playerMovement.moveSpeed = 3f;
        objectPooling.enabled = true;
    }

    private void OnGamePause()
    {
        playerMovement.isGameRunning = false;
        playerMovement.moveSpeed = 0f;
        objectPooling.enabled = false;
    }

    private void OnGameOver()
    {
        playerMovement.isGameRunning = false;
        playerMovement.moveSpeed = 0f;
        objectPooling.enabled = false;
        OnGameRestart();
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Obstacle"))
        {
            OnGameOver();
        }
    }

    private void OnGameRestart()
    {
        SetGameState(GameState.Idle);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        if(objectPooling != null)
        {
            objectPooling.ReturnObjectToPool();
        }
        else
        {
            Debug.Log("Object Pooling Referans�nda b�y�k s�k�nt� var");
        }
    }


    private void ReferenceCheck()
    {
        if (objectPooling == null)
        {
            objectPooling = FindObjectOfType<ObjectPooling>();
            if (objectPooling == null)
            {
                Debug.LogError("object pooling referans� al�nm�yor");
            }

        }
        if (playerMovement == null)
        {
            Debug.LogError(" PlayerMovement da �al��m�yor");
        }
    }
}

// �u anda object pooling referans� al�nm�yor 

// e�er karakter obje ile temas ederse karakterin hareketi duracak.
// temas kontrol� nerede sa�lanacak?
//temas kontrl� sa�land�ktan sonra i�lemler nerede d�necek??
// hareket durduktan hemen sonras�nda oyun ba�a d�necek yani otomatik olarak replay e d��mesi gerekiyor
















































/*
public void DeadGame()
{
    foreach (var objectsToSpawn_1 in playerMovement.objectsToSpawn)
    {
        objectsToSpawn_1.SetActive(false);
    }
}

public void ResetGame()
{


    playerMovement.transform.position = new Vector3(0,1,0);
    playerMovement.transform.rotation = Quaternion.identity;
    SetGameState(GameState.Idle);
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
}*/




// Buras� i�in C# event leri kullanman gerek. SS ler ald�m zaten  ClearGame olmayacak onun yerine Object Pooling y�ntemini kullan

// �u anda ObstacleS Tag ini yinr ba�layamad�m. Ama game State Controller�n �al��mas� gerekiyor yine de


/*  Bunu kullanmama gerek yok. ��nk� object pooling y�ntemi kullan�yorum
public void ClearGame()
{
    GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach(GameObject obstacle in obstacles)
    {
        Destroy(obstacle);
    }
}*/