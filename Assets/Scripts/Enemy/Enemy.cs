using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public string Name;
	public List<WeaponType> Weaknesses;
	public List<WeaponType> Resistences;
	public int Life;

	private PowerUp powerUp = PowerUpFactory.GetPowerUpNull();
	private float scale;
	public Material MaterialOverlay;

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
