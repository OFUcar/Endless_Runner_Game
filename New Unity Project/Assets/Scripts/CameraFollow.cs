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

    void LateUpdate()
    {
        float targetZ =objectToFollow.transform.position.z + zPositionOffset;
        Vector3 targetPosition = new Vector3(xPosition, yPosition , targetZ);
      
        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += (direction * cameraFollowSpeed * Time.deltaTime);
    }

}
