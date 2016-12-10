using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public string Name;
	public List<WeaponType> Weaknesses;
	public List<WeaponType> Resistences;
	public int Life;

	private PowerUp powerUp;
	private float scale;
	private Color colorOverlay;

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
