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


//transform.position = Vector3.MoveTowards(transform.position, direction, cameraFollowSpeed * Time.deltaTime);

//transform.position = Vector3(transform.position, targetPosition, cameraFollowSpeed * Time.deltaTime);

// kamera ögesi yolun oratsýnda belli açýlarda bulunacak.
//kendine ait belirli bir hýzý olacak( player = camerafollowspeed çünkü player çok ileride veya geride kalabilir)
//Player ile belirli bir mesafede olacak 

// transform.position = objectToFollow.transform.position - (Vector3.forward * 3) + (Vector3.up * 2); // kamera player ý takp ediyor
//Vector3 TargetIsPlane = new Vector3(transform.position.z + cameraFollowSpeed * Time.deltaTime, yPosition, xPosition);
//transform.position = TargetIsPlane;