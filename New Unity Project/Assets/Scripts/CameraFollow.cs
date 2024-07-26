using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject objectToFollow;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = objectToFollow.transform.position - (Vector3.forward * 3) + (Vector3.up * 2);
    }
}
