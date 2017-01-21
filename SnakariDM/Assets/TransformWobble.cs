using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TransformWobble : MonoBehaviour
{
    Transform[] wobbled;
    [SerializeField]
    private float wobbleSpeed = 5f;
    private float phase;

    void Start()
    {
        wobbled = GetComponentsInChildren<Transform>();
        phase = Random.Range(0.0f, 2f * Mathf.PI);
        wobbleSpeed = Random.Range(0f, wobbleSpeed);
    }

    void Update()
    {
        for (int i = 0; i < wobbled.Length; i++)
        {
            wobbled[i].localRotation = Quaternion.Euler(0, 0, 15f * Mathf.Sin(wobbleSpeed * Time.time + phase));
        }
    }
}
