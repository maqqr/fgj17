using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalizeRotation : MonoBehaviour {
    [SerializeField]
    private float wantedRotationMultiplier = 0.35f;
    private Rigidbody2D parentBody;

    private void Start()
    {
        parentBody = GetComponentInParent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
        this.transform.rotation = Quaternion.Euler(0, 0, parentBody.rotation * wantedRotationMultiplier);
	}
}
