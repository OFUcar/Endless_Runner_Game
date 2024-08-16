using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;

    public float xPosition = 0f;
    public float yPosition = 100f;
    public float zPositionOffset = -10f;
    public float cameraFollowSpeed = 5f;

    private static readonly Vector3 StartingPosition = new Vector3(0, 3, -2.5f);

    private void OnEnable()
    {
        GameStateController.OnGameRestart += OnGameRestart;
    }

    private void OnGameRestart()
    {
        transform.position = StartingPosition;
    }

    private void OnDisable()
    {
        GameStateController.OnGameRestart -= OnGameRestart;   
    }

    private void LateUpdate()
    {
        float targetZ =objectToFollow.transform.position.z + zPositionOffset;
        Vector3 targetPosition = new Vector3(xPosition, yPosition , targetZ);
      
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += (direction * cameraFollowSpeed * Time.deltaTime);
    }

}

// _nearestObsctale falan sýfýrkanacak
