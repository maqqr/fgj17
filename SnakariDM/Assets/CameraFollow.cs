using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField]
    List<Transform> objects;
    [SerializeField]
    Camera moveableCamera;
    [SerializeField]
    Vector3 offset;

    private float startingSize;

    private void Start()
    {
        startingSize = moveableCamera.orthographicSize;
    }



    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 total = new Vector3();
        float distance = 0f;
        for (int i = 0; i < objects.Count; i++)
        {
            total += objects[i].position;
        
        }
        total /= objects.Count;
        Debug.Log(total);
        for (int i = 0; i < objects.Count; i++)
        {
            distance += Mathf.Abs(objects[i].position.x - total.x);
        }
        Debug.Log(distance);
        moveableCamera.transform.position = total + offset;
        moveableCamera.orthographicSize = distance * 0.1f + startingSize ;
        
    }
}
