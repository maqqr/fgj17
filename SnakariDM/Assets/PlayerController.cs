using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    
    private MovementScript movementScript;
    private WobblerScript wobbler;
    private Rigidbody2D body;

    private int hits = 0;
    private float timeDown = 0f;
    [SerializeField]
    private float timeDownToFaint = 2f;
    [SerializeField]
    private string playerName = "player";
    [SerializeField]
    Text text;


	// Use this for initialization
	void Start () {
        movementScript = GetComponent<MovementScript>();
        wobbler = GetComponent<WobblerScript>();
        body = GetComponent<Rigidbody2D>();
	}
	

    public void TakeHit()
    {
        hits++;
        wobbler.WobbleForce *= 0.9f;
    }

    private void Update()
    {
        if(Mathf.Abs( body.rotation) > 45)
        {
            timeDown += Time.deltaTime;
        }
        else
        {
            timeDown = 0;
        }

        if(timeDown >= timeDownToFaint)
        {
            text.gameObject.SetActive(true);
            text.text = (playerName + " has fainted!");
        }
    }
}
