using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    private float groundLevel;

    [SerializeField]
    private Rigidbody2D target;

    void Awake()
    {
        groundLevel = transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, groundLevel, 0f);

        float scale = Mathf.Sin(Mathf.Deg2Rad * Mathf.Abs(target.rotation));
        transform.localScale = new Vector3(1f + scale, 1f, 1f);
    }
}
