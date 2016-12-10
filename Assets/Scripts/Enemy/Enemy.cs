using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public List<WeaponType> Weaknesses = new List<WeaponType>();
	public List<WeaponType> Resistences = new List<WeaponType>();
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
