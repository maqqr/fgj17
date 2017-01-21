using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTaker : MonoBehaviour {
    [SerializeField]
    List<Sprite> sprites = new List<Sprite>();
	// Use this for initialization
	void Start () {
        if (sprites.Count == 0)
            throw new UnityException("Add sprites to the list!");
        int selected = Random.Range(0, sprites.Count);
        GetComponent<SpriteRenderer>().sprite = sprites[selected];
	}
	
}
