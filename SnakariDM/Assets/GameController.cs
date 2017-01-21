using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public event Action OnRoundEnd;
    public event Action<PlayerController> OnPlayerFaint;

    [SerializeField]
    private PlayerController p1;

    [SerializeField]
    private PlayerController p2;

    [SerializeField]
    Text text;

    [SerializeField]
    private float endWaitTime = 3f;

    private DrunkEffect drunkEffect;
    private BackgroundMusic music;

    private float targetAvgDrunkLevel = 0f;
    private float avgDrunkLevel = 0f;

    private bool ending = false;
    private float endCounter = 0f;

    void Start()
    {
        p1.onFaint += PlayerFainted;
        p2.onFaint += PlayerFainted;

        drunkEffect = Camera.main.GetComponent<DrunkEffect>();
        music = GetComponent<BackgroundMusic>();
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
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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

        targetAvgDrunkLevel = (p1.DrunkLevel + p2.DrunkLevel) / 2f;
        avgDrunkLevel += (targetAvgDrunkLevel - avgDrunkLevel) * Time.deltaTime * 0.5f;

        drunkEffect.waveScale = new Vector2(1f, 1f) * 0.08f * avgDrunkLevel;
        drunkEffect.ghostImage = 0.3f * avgDrunkLevel;
        music.pitchChange = 0.3f * avgDrunkLevel;
        Time.timeScale = 1.0f + 0.5f * Mathf.Sin(Time.unscaledTime*0.5f);
    }

    public void EndGame()
    {
        if (OnRoundEnd != null)
        {
            OnRoundEnd();
        }
    }
}