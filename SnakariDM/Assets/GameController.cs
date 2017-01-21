using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

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
        p1.onFaint += EndGame;
        p2.onFaint += EndGame;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if(ending)
        {
            endCounter += Time.deltaTime;
        }
        if(endCounter >= endWaitTime)
        {
            SceneManager.LoadScene("Menu");
        }
	}

    public void EndGame(PlayerController pl)
    {
        text.gameObject.SetActive(true);
        text.text = (pl.PlayerName + " has fainted!");
        ending = true;
    }

}
