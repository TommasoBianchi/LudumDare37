using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDataNull : PowerUpData {

	public PowerUpDataNull() {
		this.Duration = 10;
	}

	public void OnFinish() {
		
	}

	public void OnAttack() {
		
	}

	public bool IsNull() {
		return true;
	}
}
