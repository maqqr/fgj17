using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    [SerializeField]
    Image bg;

    [SerializeField]
    float minA = 0.5f;
    [SerializeField]
    float maxA = 25f;
    [SerializeField]
    float change = 0.1f;
    float dir = 1;
    [SerializeField]
    Camera cam;
    [SerializeField]
    Transform rotateAround;
    [SerializeField]
    float rotationSpeed = 5f;
    [SerializeField]
    bool rotateRight = true;
    [SerializeField]
    bool rotateUp = false;
    private float timePassed = 0f;

    private void Start()
    {
        Color c = bg.color;
        c.a = minA / 255f;
        bg.color =  c;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("ShaderTests");
    }


    public void EMT()
    {
        bg.color = Random.ColorHSV();
    }


    public void ExitGame()
    {
        Application.Quit();
    }

    private void Update()
    {
        float dTime = Time.deltaTime;
        Color col = bg.color;
        col.a += change /255f *dTime  * dir;
        if(dir > 0 && col.a >= maxA / 255f)
        {
            dir = dir * -1;

        }
        else if(dir < 0 && col.a <= minA / 255f)
        {
            dir = dir * -1;
        }
        bg.color = col;
        timePassed += dTime;
        if(timePassed > 7f)
        {
            rotateUp = true;
        }

        if(rotateRight)
        {
            rotateAround.transform.Rotate(rotateAround.up, rotationSpeed * dTime);
        }
            
        
        if(rotateUp)
        {
            rotateAround.transform.Rotate(rotateAround.right, rotationSpeed * dTime);
        }
    }
}
