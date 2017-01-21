using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private AudioSource source;

    public float pitchChange = 0f;

    void Awake()
    {
        source = GetComponent<AudioSource>();
        source.Play();
    }

    void Update()
    {
        source.pitch = 1.0f + (Mathf.Sin(Time.unscaledTime * 0.5f) * pitchChange);
    }
}
