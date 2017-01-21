using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMoveScript : MonoBehaviour {

    [SerializeField]
    private float directionSpeed = -2f;
    [SerializeField]
    private float destroyX = -48f;

	void Update () {
        transform.position += Vector3.right * directionSpeed * Time.deltaTime;
        float sign = Mathf.Sign(directionSpeed);
        if(sign * transform.position.x >= sign * destroyX)
        {
            Destroy(gameObject);
        }

	}
}
