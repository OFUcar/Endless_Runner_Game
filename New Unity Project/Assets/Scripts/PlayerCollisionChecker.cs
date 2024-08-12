using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionChecker : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public ObjectPooling objectPooling;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"�arp��an : {other.gameObject.name}");

        List<GameObject> activeObjects = objectPooling.GetAllActiveObject();
        Debug.Log("Aktif : ");
        foreach (GameObject obj in activeObjects)
        {
            Debug.Log($"- {obj.name}");
        }

        if (activeObjects.Contains(other.gameObject))
        {
            Debug.Log("�arpma oldu����");
            DeadGame();
        }
        else
        {
            Debug.Log("�arp��an obje aktif de�il.");
        }
    }

    private void DeadGame()
    {
        Debug.Log("�ld�n ��k");
    }
}















    /*
    private void OnTriggerEnter(Collider other)
    {
        if (ObjectPooling.GetAllActiveObject().Contains(other.gameObject))
        {
            Debug.Log("�arpma oldu");
            DeadGame();
        }
    }

    private void DeadGame()
    {
        Debug.Log("�ld�n ��k");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"�arp��an obje ad�: {other.gameObject.name}");

        List<GameObject> activeObjects = objectPooling.GetAllActiveObject();
        Debug.Log("Aktif Objeler: ");
        foreach (GameObject obj in activeObjects)
        {
            Debug.Log($"- {obj.name}");
        }

        if (activeObjects.Contains(other.gameObject))
        {
            Debug.Log("�arpma oldu");
            DeadGame();
        }
        else
        {
            Debug.Log("�arp��an obje aktif de�il.");
        }
    }

    private void DeadGame()
    {
        Debug.Log("�ld�n ��k");
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
                Debug.Log("�arp��ma ger�ekle�ti");
                break;
                // Burada DeadGame() methodu �a��r�lacak
            }
        }
    } 

    private bool IsColliding(GameObject obstacle)
    {
        Collider obstacleCollider = obstacle.GetComponent<Collider>();
        return _playerCollider.bounds.Intersects(obstacleCollider.bounds);
    }
}*/
