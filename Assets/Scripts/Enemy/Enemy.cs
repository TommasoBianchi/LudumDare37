using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public List<WeaponType> Weaknesses;
	public List<WeaponType> Resistences;
	public int Life;
	public PowerUpData PowerUpData = PowerUpFactory.GetPowerUpNull();

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
