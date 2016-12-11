using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDataLife : PowerUpData {

	public PowerUpDataLife() {
		this.Duration = 0.0f;
		this.type = 3;
	}

	override public void OnStart() {
		Globals.GetPlayerController().addLife(1);
	}

	override public void OnFinish() {
		
	}

	override public void OnAttack() {
		
	}
}
