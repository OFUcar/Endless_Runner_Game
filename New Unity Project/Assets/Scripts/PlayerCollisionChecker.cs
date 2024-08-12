using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionChecker : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public ObjectPooling objectPooling;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Çarpýþan : {other.gameObject.name}");

        List<GameObject> activeObjects = objectPooling.GetAllActiveObject();
        Debug.Log("Aktif : ");
        foreach (GameObject obj in activeObjects)
        {
            Debug.Log($"- {obj.name}");
        }

        if (activeObjects.Contains(other.gameObject))
        {
            Debug.Log("Çarpma olduðððð");
            DeadGame();
        }
        else
        {
            Debug.Log("Çarpýþan obje aktif deðil.");
        }
    }

    private void DeadGame()
    {
        Debug.Log("Öldün Çýk");
    }
}















    /*
    private void OnTriggerEnter(Collider other)
    {
        if (ObjectPooling.GetAllActiveObject().Contains(other.gameObject))
        {
            Debug.Log("Çarpma oldu");
            DeadGame();
        }
    }

    private void DeadGame()
    {
        Debug.Log("Öldün Çýk");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Çarpýþan obje adý: {other.gameObject.name}");

        List<GameObject> activeObjects = objectPooling.GetAllActiveObject();
        Debug.Log("Aktif Objeler: ");
        foreach (GameObject obj in activeObjects)
        {
            Debug.Log($"- {obj.name}");
        }

        if (activeObjects.Contains(other.gameObject))
        {
            Debug.Log("Çarpma oldu");
            DeadGame();
        }
        else
        {
            Debug.Log("Çarpýþan obje aktif deðil.");
        }
    }

    private void DeadGame()
    {
        Debug.Log("Öldün Çýk");
    }
}



 * 
public class PlayerCollisionChecker : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public ObjectPooling objectPooling; 

    private Collider _playerCollider;

    private void Start()
    {
        _playerCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        CheckForCollision();
    }

    private void CheckForCollision()
    {
        List<GameObject> activeObject = objectPooling.GetAllActiveObject();
        Debug.Log("Aktif obje 1: " +activeObject.Count);

        foreach (GameObject obj in activeObject) 
        {
            Debug.Log("Aktif obje 2: " + activeObject.Count);
            if (!IsColliding(obj)) 
            {
                Debug.Log("Çarpýþma gerçekleþti");
                break;
                // Burada DeadGame() methodu çaðýrýlacak
            }
        }
    } 

    private bool IsColliding(GameObject obstacle)
    {
        Collider obstacleCollider = obstacle.GetComponent<Collider>();
        return _playerCollider.bounds.Intersects(obstacleCollider.bounds);
    }
}*/
