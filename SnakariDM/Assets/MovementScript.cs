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
    [SerializeField]
    private Vector2 centerOfMass = new Vector2(0, -0.2f);

    private bool rooted = true;
    private SpriteRenderer spriteRend;


	// Use this for initialization
	void Start () {
        spriteRend = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        body.centerOfMass = centerOfMass;

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Move();

        float jump = Input.GetKeyDown(KeyCode.UpArrow) ? 1.0f : 0.0f; //Input.GetAxis("Vertical");


        rooted = Physics2D.Raycast(transform.position + -transform.up * 1.02f , -transform.up, 0.5f);
        //rooted = Physics.SphereCast(transform.position + new Vector3(0, -1.05f, 0), 0.15, );
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
