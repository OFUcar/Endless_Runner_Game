using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public GameObject objectPrefab;
    public int poolSize = 300;

    private List<GameObject> pool;

    void Awake()
    {
        pool = new List<GameObject>();

        for ( int i = 0; i < poolSize; i++ )
        {
            GameObject poolGameObject = Instantiate(objectPrefab);
            poolGameObject.SetActive(false);
            pool.Add( poolGameObject );
        }
    }

    public GameObject GetPooledObject() 
    {
        foreach (GameObject poolGameObject in pool)
        {
            if (!poolGameObject.activeInHierarchy)
            {
                return poolGameObject;
            }
        }

        GameObject newPoolGameObject = Instantiate(objectPrefab);
        newPoolGameObject.SetActive(false);
        pool.Add(newPoolGameObject);
        return newPoolGameObject;
    }

    public void ReturnObjectToPool( GameObject poolGameObject)
    {
        poolGameObject.SetActive(false);
    }

    /*internal static void ReturnObjectToPool()
    {
        throw new NotImplementedException();
    }*/
}
