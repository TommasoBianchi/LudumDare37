using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDataSpeed : PowerUpData {

	public PowerUpDataSpeed(float mult, float duration) {
		this.type = 1;
		this.mult = mult;
		this.Duration = duration;
	}

	public void OnPickup () {
		
	}

	override public void OnStart() {
		Debug.Log("SPEED INCREASED");
		PlayerController pc = Globals.GetPlayerController();
		pc.Speed = Constants.PLAYER_BASE_SPEED * this.mult;
		Globals.GetPlayer().GetComponent<SpriteRenderer>().material.SetColor("_Color", Constants.COLOR_OVERLAY_BONUS_SPEED);
	}

	override public void OnFinish() {
		Debug.Log("SPEED SET BACK TO NORMAL");
		PlayerController pc = Globals.GetPlayerController();
		pc.Speed = Constants.PLAYER_BASE_SPEED;
		Globals.GetPlayer().GetComponent<SpriteRenderer>().material.SetColor("_Color", Constants.COLOR_OVERLAY_DEFAULT);
	}

	override public void OnAttack() {
		
	}
}
