using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PovDispenser : MonoBehaviour {

    float timeSpent;
    float timeToDestroy = 0.5f;
    private Pool povPool;

    public float TimeToDestroy
    {
        get
        {
            return timeToDestroy;
        }

        set
        {
            timeToDestroy = value;
        }
    }

    internal Pool PovPool
    {
        get
        {
            return povPool;
        }

        set
        {
            povPool = value;
        }
    }

    private void Update()
    {
        timeSpent += Time.deltaTime;
        if(timeSpent >= TimeToDestroy)
        {
            Destroy(this);
            PovPool.Return(gameObject);
            
        }

    }

}
