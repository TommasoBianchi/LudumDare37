using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public string Name;
	public List<WeaponType> Weaknesses;
	public List<WeaponType> Resistences;
	public int Life;

	public PowerUp PowerUp = PowerUpFactory.GetPowerUpNull();
	public float Scale;
	public Material MaterialOverlay;

	void Start () {
		
	}
	
	void Update () {
		Move ();
		Attack ();
	}

	void Move() {

	}

	void Attack() {

	}
}
