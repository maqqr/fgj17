using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    [SerializeField]
    private float movementSpeed = 25f;

    private Rigidbody2D body;
    [SerializeField]
    private float movementForceModifierRotation = 50;
    [SerializeField]
    private float jumpForce = 100f;
    [SerializeField]
    private Vector2 centerOfMass = new Vector2(0, -0.2f);
    [SerializeField]
    private float wanderSpeed = 2f;

    [SerializeField]
    private FistController rightFist;
    [SerializeField]
    private FistController leftFist;
    [SerializeField]
    private string movementAxis = "Horizontal";
    [SerializeField]
    private KeyCode punchRight = KeyCode.Return;
    [SerializeField]
    private KeyCode punchLeft = KeyCode.RightControl;
    [SerializeField]
    private KeyCode jumpButton = KeyCode.UpArrow;
    [SerializeField]
    private SpriteRenderer shoeL;
    [SerializeField]
    private SpriteRenderer shoeR;

    private static Vector2 HEAD = new Vector2(0, 0.45f);
    private bool rooted = true;
    private SpriteRenderer spriteRend;

    private Facing facing = Facing.Left;
    public Facing Facing { get
        {
            return facing;

        }
        set
        {
            UpdateFacing(value);
            facing = value;
        }
    }

    private void UpdateFacing(Facing value)
    {
        spriteRend.flipX = value == Facing.Right;
        //shoeL.flipX = value == Facing.Right;
        //shoeR.flipX = value == Facing.Left;
    }


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

        float jump = Input.GetKeyDown(jumpButton) ? 1.0f : 0.0f; //Input.GetAxis("Vertical");

        MoveByRotation();

        rooted = Physics2D.Raycast(transform.position + -transform.up * 1.15f , -transform.up, 0.25f);
        //rooted = Physics.SphereCast(transform.position + new Vector3(0, -1.05f, 0), 0.15, );
        if (rooted)
        {

            Vector3 force = transform.up * jump * jumpForce;
            //body.AddForce(force, ForceMode2D.Impulse);

            if (jump > 0)
                body.velocity = new Vector2(force.x, force.y);
        }

      

        if(Input.GetKeyDown(punchRight))
        {
            rightFist.ThrowAPunch(body, Facing);
        }
        if(Input.GetKeyDown(punchLeft))
        {
            leftFist.ThrowAPunch(body, Facing);
        }

    }

    private void MoveByRotation()
    {
        float direction = Mathf.Sign(body.rotation) * -1f;
        direction = Mathf.Min(45, Mathf.Abs( body.rotation)) / 45 * direction;
        body.position += new Vector2(wanderSpeed * direction * Time.deltaTime, 0);
    }

    private void Move()
    {
        Vector2 movement = new Vector2();
        float fdTime = Time.fixedDeltaTime;
        movement.x = Input.GetAxis(movementAxis);

        //transform.position += movement * Time.deltaTime * movementSpeed;
        body.AddForce(movement * fdTime * movementSpeed, ForceMode2D.Force);

        float torque = movement.x * fdTime * movementForceModifierRotation;
        // body.AddTorque(torque);
        Vector2 d = new Vector2(transform.up.x, transform.up.y);
        Vector2 headForce = body.position + d * 0.35f;
        float rotationMultiplier = 1 - Mathf.Abs(body.rotation) / 180f;

        Vector2 force = new Vector2(-torque, 0) * rotationMultiplier;
        body.AddForceAtPosition(force, headForce, ForceMode2D.Force);
        if (movement.x > 0)
        {
            Facing = Facing.Right;
            //transform.rotation.SetEulerAngles(transform.rotation.x, -180f, transform.rotation.z);    
        
        //transform.rotation = new Vector3(transform.rotation.x, -180f, transform.rotation.z);
        }
        else if (movement.x < 0)
        {
            Facing = Facing.Left;
           // transform.rotation.SetEulerAngles(transform.rotation.x, 0, transform.rotation.z);
        }

    }
}
