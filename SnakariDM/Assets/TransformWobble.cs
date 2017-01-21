using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformWobble : MonoBehaviour {
    Transform[] wobbled;
    [SerializeField]
    private float wobbleSpeed = 5f;
    private float uniqWobble;

	// Use this for initialization
	void Start () {
        wobbled = GetComponentsInChildren<Transform>();
        uniqWobble = Random.Range(-0.25f, 0.75f);
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < wobbled.Length; i++)
        {
            wobbled[i].rotation *= Quaternion.Euler(0,0, wobbleSpeed * Mathf.Sin(wobbleSpeed * Time.time * uniqWobble) * Time.deltaTime);
        }
        transform.rotation *= Quaternion.Euler(0, 0, wobbleSpeed * Mathf.Sin(wobbleSpeed * Time.time * uniqWobble) * Time.deltaTime);
	}
}
