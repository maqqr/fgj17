using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField]
    Image bg;

    public void StartGame()
    {
        SceneManager.LoadScene("MainFight");
    }


    public void EMT()
    {
        bg.color = Random.ColorHSV();
    }


    public void ExitGame()
    {
        Application.Quit();
    }
}
