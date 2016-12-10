using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomizeSprite : MonoBehaviour {

    public Sprite[] sprites;

	void Start () {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[(Random.Range(0, sprites.Length))];
	}
}
