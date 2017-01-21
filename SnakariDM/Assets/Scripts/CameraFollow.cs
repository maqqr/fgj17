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
    [SerializeField]
    private float maxSize = 10f;

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
        for (int i = 0; i < objects.Count; i++)
        {
            distance += Mathf.Abs(objects[i].position.x - total.x);
        }
       
        float size = Mathf.Min(maxSize, distance * 0.15f + startingSize);
        moveableCamera.orthographicSize = size;
        Vector3 scaled = total * (1 - (size - startingSize) / (maxSize - startingSize));

        Vector3 center = scaled + offset;

        moveableCamera.transform.position = center;

    }
}
