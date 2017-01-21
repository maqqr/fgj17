using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public event Action<PlayerController> onFaint;

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
    private SpriteRenderer shirt;

    [SerializeField]
    private SpriteRenderer x1;
    [SerializeField]
    private SpriteRenderer x2;
    [SerializeField]
    private int playerNumber = 1;
    private bool fainted = false;

    private float drunkLevel;

    public float DrunkLevel
    {
        get { return drunkLevel; }
        set { drunkLevel = Mathf.Max(0f, Mathf.Min(1f, value)); }
    }

    public string PlayerName
    {
        get
        {
            return playerName;
        }

        set
        {
            playerName = value;
        }
    }

    public int PlayerNumber
    {
        get
        {
            return playerNumber;
        }

        set
        {
            playerNumber = value;
        }
    }


    // Use this for initialization
    void Start () {
        movementScript = GetComponent<MovementScript>();
        wobbler = GetComponent<WobblerScript>();
        body = GetComponent<Rigidbody2D>();
        shirt.color = UnityEngine.Random.ColorHSV();
	}
	

    public void TakeHit()
    {
        hits++;
        wobbler.WobbleForce *= 0.9f;
    }

    private void Update()
    {
        if (fainted) return;
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
            Faint();
        }
    }

    private void Faint()
    {
        movementScript.enabled = false;
        wobbler.enabled = false;
        fainted = true;
        x1.gameObject.SetActive(true);
        x2.gameObject.SetActive(true);
        if (onFaint != null)
            onFaint(this);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Bottle")
        {
            DrunkLevel += 0.2f;
            Destroy(collision.collider.gameObject);
        }
    }
}
