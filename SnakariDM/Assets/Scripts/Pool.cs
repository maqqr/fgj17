using System.Collections.Generic;
using UnityEngine;

internal class Pool : MonoBehaviour
{
    Queue<GameObject> pool = new Queue<GameObject>();
    [SerializeField]
    GameObject pooled;
    [SerializeField]
    int startPool = 15;

    private void Start()
    {
        MakeObjects(startPool);
    }

    private void MakeObjects(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject gobj = GameObject.Instantiate(pooled);
            gobj.SetActive(false);
            pool.Enqueue(gobj);
        }
    }

    public GameObject Get()
    {
        if(pool.Count > 0)
        {
            GameObject obj = pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }
        MakeObjects(10);
        return Get();
       
    }

    public void Return(GameObject gobj)
    {
        gobj.SetActive(false);
        pool.Enqueue(gobj);

    }


}