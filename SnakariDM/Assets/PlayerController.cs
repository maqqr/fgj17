﻿using System;
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

    private bool fainted = false;


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
        if (onFaint != null)
            onFaint(this);
    }
}