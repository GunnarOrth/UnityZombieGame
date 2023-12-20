using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject ObjectToPool;
    public int amountToPool;
    public static ObjectPool sharedInstance;

    private void Awake()
    {
        sharedInstance = this;
    }

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for(int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(ObjectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject getPooledObject()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if(!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
