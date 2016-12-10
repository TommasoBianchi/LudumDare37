using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager {

	private PowerUp currentPowerUp;
	private float activationTime = 0;
	private float totalDeltaTime = 0;
	
	public void SetPowerUp(PowerUp powerUp) {
		this.currentPowerUp = powerUp;
	}

	public PowerUp GetPowerUp() {
		return this.currentPowerUp;
	}

	public void Update() {
		this.totalDeltaTime += Time.deltaTime;
		if (this.totalDeltaTime - this.activationTime > this.currentPowerUp.Duration) {
			this.currentPowerUp = PowerUpFactory.GetPowerUpNull();
		}
	}
}
