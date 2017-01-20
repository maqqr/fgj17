using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WobblerScript : MonoBehaviour {

    private Rigidbody2D body;

    private float lastChange = 0f;
    private float nextChange = 0.05f;
    private float wobble = 1f;

    [SerializeField]
    private float wobbleForce = 25f;

    public float WobbleForce
    {
        get
        {
            return wobbleForce;
        }

        set
        {
            wobbleForce = value;
        }
    }

    // Use this for initialization
    void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float dTime = Time.fixedDeltaTime;
        lastChange += dTime;

      //if(lastChange > nextChange)
      //  {
            float direction = -1f* Mathf.Sign(body.rotation);
            wobble = direction * (Mathf.Sin(Time.time * 0.01f) * 0.5f +0.5f) *  0.01f;
            lastChange = 0f;
      //  }
      //  body.rotation += wobble * dTime * wobbleForce;
        body.AddTorque(wobble *dTime * WobbleForce);

    }
}
