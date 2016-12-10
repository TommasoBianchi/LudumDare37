using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : PowerUp {

	private float mult;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public PowerUpSpeed(float mult) {
		this.mult = mult;
	}

	public void OnPickup () {
		
	}

	public void OnStart() {
		PlayerController pc = Globals.GetPlayerController();
		pc.Speed = Constants.PLAYER_BASE_SPEED * this.mult;
	}

	public void OnFinish() {
		PlayerController pc = Globals.GetPlayerController();
		pc.Speed = Constants.PLAYER_BASE_SPEED;
	}

	public void OnAttack() {
		
	}
}
