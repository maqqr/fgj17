using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public event Action OnRoundEnd;
    public event Action<PlayerController> OnPlayerFaint;

    [SerializeField]
    PlayerController p1;
    [SerializeField]
    PlayerController p2;

    [SerializeField]
    Text text;

    [SerializeField]
    private float endWaitTime = 3f;
    private bool ending = false;
    private float endCounter = 0f;


    void Start () {
        p1.onFaint += PlayerFainted;
        p2.onFaint += PlayerFainted;
	}

    private void PlayerFainted(PlayerController obj)
    {
        if (OnPlayerFaint != null)
            OnPlayerFaint(obj);
        ending = true;
        text.gameObject.SetActive(true);
        text.text = (obj.PlayerName + " has fainted!");
    }

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (ending)
        {
            endCounter += Time.deltaTime;
        }
        if (endCounter >= endWaitTime)
        {
            EndGame();

        }
    }

    public void EndGame()
    {
        if(OnRoundEnd != null)
        {
            OnRoundEnd();
        }


    }

}
