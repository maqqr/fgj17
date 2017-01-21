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

    public const float levelWidth = 50f;

    public AudioClip[] punchSounds;
    public AudioClip[] hurtSounds;
    public AudioClip drinkSound;

    [SerializeField]
    private GameObject leftCarPrefab;

    [SerializeField]
    private GameObject rightCarPrefab;

    [SerializeField]
    private PlayerController p1;

    [SerializeField]
    private PlayerController p2;

    [SerializeField]
    Text text;

   // [SerializeField]
    Weather weather;
    [SerializeField]
    private bool useWeather = true;

    [SerializeField]
    private float endWaitTime = 3f;

    [SerializeField]
    private List<GameObject> droppablePrefabs = new List<GameObject>();
    [SerializeField]
    private float dropMinTime = 10f;
    [SerializeField]
    private float dropMaxTime = 20f;

    private DrunkEffect drunkEffect;
    private BackgroundMusic music;

    private float targetAvgDrunkLevel = 0f;
    private float avgDrunkLevel = 0f;

    private bool ending = false;
    private float endCounter = 0f;

    private float carSpawnTimer = 5f;

    private float dropSpawnTimer = 0f;
    private float dropNextTime;


    void Start()
    {
        p1.onFaint += PlayerFainted;
        p2.onFaint += PlayerFainted;

        drunkEffect = Camera.main.GetComponent<DrunkEffect>();
        music = GetComponent<BackgroundMusic>();

        if (weather == null && useWeather)
        {
            weather = PickRandomWeather("Weathers");

            GameObject pSystem = Resources.Load<GameObject>(weather.particleSystem);
            Instantiate(pSystem);
            GameObject pavement = GameObject.Find("Pavement");
            BoxCollider2D coll = pavement.GetComponent<BoxCollider2D>();
            PhysicsMaterial2D newMat = PhysicsMaterial2D.Instantiate(coll.sharedMaterial);
            newMat.friction = weather.groundFriction;
            newMat.bounciness = weather.groundBounciness;
            coll.sharedMaterial = newMat;

        }

        RandomizeDropTime();
    }

    private void RandomizeDropTime()
    {
        dropNextTime = UnityEngine.Random.Range(dropMinTime, dropMaxTime);
    }

    private Weather PickRandomWeather(string v)
    {
        TextAsset file = Resources.Load<TextAsset>(v);

        Weathers weather = JsonUtility.FromJson<Weathers>(file.text);
        Weather selected = weather.weathers[UnityEngine.Random.Range(0, weather.weathers.Length)];
        return selected;

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
        drunkEffect.ghostImage = 0.4f * avgDrunkLevel;
        music.pitchChange = 0.4f * avgDrunkLevel;
        Time.timeScale = 1.0f + 0.6f * Mathf.Sin(Time.unscaledTime*0.5f) * avgDrunkLevel;

        carSpawnTimer -= Time.deltaTime;
        if (carSpawnTimer < 0f)
        {
            carSpawnTimer = 4f + UnityEngine.Random.value * 10f;
            GameObject car = UnityEngine.Random.value > 0.5f ? leftCarPrefab : rightCarPrefab;
            Instantiate(car);
        }

        dropSpawnTimer += Time.deltaTime;
        if(dropSpawnTimer >= dropNextTime)
        {
            dropSpawnTimer -= dropNextTime;
            GameObject bas = droppablePrefabs[UnityEngine.Random.Range(0, droppablePrefabs.Count)];
            GameObject newOne = GameObject.Instantiate(bas);
            newOne.transform.position = new Vector3(UnityEngine.Random.Range(levelWidth * -0.5f, levelWidth * 0.5f), 25, -2);
            newOne.GetComponent<Rigidbody2D>().AddTorque(10f, ForceMode2D.Impulse);
            RandomizeDropTime();
        }

    }

    public void EndGame()
    {
        if (OnRoundEnd != null)
        {
            OnRoundEnd();
        }
    }

    public void PlayRandomSound(AudioClip[] clips, float volume = 0.5f)
    {
        AudioClip clip = clips[UnityEngine.Random.Range(0, clips.Length)];
        PlayRandomSound(clip);
    }

    public void PlayRandomSound(AudioClip clip, float volume = 0.5f)
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
    }
}