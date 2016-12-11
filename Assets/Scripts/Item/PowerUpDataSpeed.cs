using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDataSpeed : PowerUpData {

	private float mult;
	public PowerUpDataSpeed(float mult) {
		this.mult = mult;
	}

	public void OnPickup () {
		
	}

	public void OnStart() {
		PlayerController pc = Globals.GetPlayerController();
		pc.Speed = Constants.PLAYER_BASE_SPEED * this.mult;
		GameObject player = Globals.GetPlayer();
		player.GetComponent<SpriteRenderer>().material = Globals.PowerUpAttackMaterial;
	}

	public void OnFinish() {
		PlayerController pc = Globals.GetPlayerController();
		pc.Speed = Constants.PLAYER_BASE_SPEED;
		GameObject player = Globals.GetPlayer();
		player.GetComponent<SpriteRenderer>().material = Globals.PowerUpAttackMaterial;
	}

	public void OnAttack() {
		
	}
}
