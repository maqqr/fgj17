using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class DrunkEffect : MonoBehaviour
{
    [SerializeField]
    private Material material;

    [SerializeField]
    public Vector2 waveScale = new Vector2(0.02f, 0.02f);

    [SerializeField]
    public Vector2 timeScale = new Vector2(0.7f, 0.7f);

    public float ghostImage = 0f;

    void Awake()
    {
        material = Instantiate(material);
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        material.SetVector("_WaveScale", new Vector4(waveScale.x, waveScale.y, 0f, 0f));
        material.SetVector("_TimeScale", new Vector4(timeScale.x, timeScale.y, 0f, 0f));
        material.SetFloat("_GhostImage", ghostImage);
        Graphics.Blit(source, destination, material);
    }
}
