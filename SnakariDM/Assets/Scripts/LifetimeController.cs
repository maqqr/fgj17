using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifetimeController : MonoBehaviour {

    private GameController gc;
    private int[] playerWins = new int[2];
    private Text[] playerTexts;


    private Canvas canvas;




    private static LifetimeController Instance = null;
    void Start ()
    {
        DontDestroyOnLoad(gameObject);
        if (canvas == null)
        {
            canvas = GetComponentInChildren<Canvas>();

            canvas.worldCamera = Camera.main;
        }
        if (playerTexts == null)
        {
            playerTexts = GetComponentsInChildren<Text>();
        }
        UpdateTexts();
        if(Instance == null)
        {
            SceneManager.sceneLoaded += NewSceneStarted;
            
            Instance = this;
        }
        if(SceneManager.GetActiveScene().name == "Load")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
       

    }



    private void UpdateTexts()
    {
        for (int i = playerTexts.Length-1; i >= 0; i--)
        {
            playerTexts[i].text = "P" + (2 - i) + ": " + playerWins[i];
        }
    }

    private void NewSceneStarted(Scene scene, LoadSceneMode loadmode)
    {
        if(scene.name != "Menu")
        {
            gc = GameObject.FindObjectOfType<GameController>();
            
            gc.OnPlayerFaint += PlayerFaints;
            gc.OnRoundEnd += RoundEnds;
            canvas.gameObject.SetActive(true);
        }
        else if(scene.name == "Menu")
        {
            playerWins = new int[2];
            UpdateTexts();
            canvas.gameObject.SetActive(false);
        }
    }

    private void RoundEnds()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void PlayerFaints(PlayerController obj)
    {

        playerWins[obj.PlayerNumber - 1]++;
        UpdateTexts();

    }

    // Update is called once per frame
    void Update () {


    }
}
