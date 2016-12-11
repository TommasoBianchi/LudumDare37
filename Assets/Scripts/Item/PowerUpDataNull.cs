using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDataNull : PowerUpData {

	public PowerUpDataNull() {
		this.Duration = 10;
		this.type = 2;
	}

	override public void OnStart() {
		
	}

	override public void OnFinish() {
		
	}

	override public void OnAttack() {
		
	}

}
