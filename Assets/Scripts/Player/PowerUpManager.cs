using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager {

	private PowerUpData currentPowerUp;
	private float totalDeltaTime = 0;
	
	public void SetPowerUp(PowerUpData powerUp) {
		this.currentPowerUp = powerUp;
		Debug.Log("Power up setted");
		powerUp.OnStart();
	}

	public PowerUpData GetPowerUp() {
		return this.currentPowerUp;
	}

	public void Update() {
		if (currentPowerUp.type != 2) { // If not powerup null
			this.totalDeltaTime += Time.deltaTime;
			if (this.totalDeltaTime > this.currentPowerUp.Duration) {
				this.currentPowerUp.OnFinish();
				Debug.Log("Power up finished");
				this.currentPowerUp = PowerUpFactory.GetPowerUpNull();
				this.totalDeltaTime = 0;
			}
		}
	}
}
