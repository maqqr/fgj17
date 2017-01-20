using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    [SerializeField]
    private float movementSpeed = 25f;
    [SerializeField]
    private Rigidbody2D body;
    [SerializeField]
    private float movementForceModifierRotation = 50;
    [SerializeField]
    private float jumpForce = 100f;

    private bool rooted = true;
    private SpriteRenderer spriteRend;


	// Use this for initialization
	void Start () {
        spriteRend = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Move();

        float jump = Input.GetKeyDown(KeyCode.UpArrow) ? 1.0f : 0.0f; //Input.GetAxis("Vertical");

        if (rooted)
        {

            Vector3 force = transform.up * jump * jumpForce;
            body.AddForce(force, ForceMode2D.Impulse);
        }
    }

    private void Move()
    {
        Vector3 movement = new Vector3();

        movement.x = Input.GetAxis("Horizontal");

        transform.position += movement * Time.deltaTime * movementSpeed;

        float torque = movement.x * Time.deltaTime * movementForceModifierRotation;
        Debug.Log(torque);
        body.AddTorque(torque);

        if (movement.x > 0)
            spriteRend.flipX = true;
        else if (movement.x < 0)
            spriteRend.flipX = false;
    }
}
