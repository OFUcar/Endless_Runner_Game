using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        foreach (GameObject obj in activeObject) 
        {
            if (!IsColliding(obj)) 
            {
                Debug.Log("�arp��ma ger�ekle�ti");
                // Burada DeadGame() methodu �a��r�lacak
            }
        }
    }

    private bool IsColliding(GameObject obstacle)
    {
        Collider obstacleCollider = obstacle.GetComponent<Collider>();
        return _playerCollider.bounds.Intersects(obstacleCollider.bounds);
    }
}
