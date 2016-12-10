using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpNull : PowerUp {

	public PowerUpNull() {
		this.Duration = 10;
	}

	public void OnStart() {
		
	}

	public void OnFinish() {
		
	}

	public void OnAttack() {
		
	}

	public bool IsNull() {
		return true;
	}
}
