using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public string name;
	public List<WeaponType> weaknesses;
	public List<WeaponType> resistences;
	public int life;
	public PowerUp powerUp;
	public float scale;
	public Color colorOverlay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Move ();
		Attack ();
	}

	void Move() {

	}

	void Attack() {

	}
}
