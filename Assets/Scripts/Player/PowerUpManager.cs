using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager {

	private PowerUp currentPowerUp;
	private float activationTime;
	
	public void SetPowerUp(PowerUp powerUp) {
		this.currentPowerUp = powerUp;
	}

	public PowerUp GetPowerUp() {
		return this.currentPowerUp;
	}

	public void Update() {
		throw new System.NotImplementedException ();
	}
}
