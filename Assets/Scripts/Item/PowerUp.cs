﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour, Item {

	public float duration;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	public void OnPickup () {
		throw new System.NotImplementedException ();
	}

	public void OnStart() {
		throw new System.NotImplementedException ();
	}

	public void OnFinish() {
		throw new System.NotImplementedException ();
	}

	public void OnAttack() {
		throw new System.NotImplementedException ();
	}
}
