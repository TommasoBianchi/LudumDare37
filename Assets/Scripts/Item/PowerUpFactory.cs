using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFactory {

	public static PowerUp GetRandomPowerUp() {
		int num = Random.Range(0, 100);
		PowerUp powerUp = GetPowerUpNull();

		if (num <= 10) {
			powerUp = GetPowerUpAttack();
		}
		else if (num <= 20) {
			powerUp = GetPowerUpSpeed();
		}

		return powerUp;
	}

	public static PowerUp GetPowerUpSpeed() {
		float mult = Random.Range(Constants.POWER_UP_MIN_SPEED_MULT, Constants.POWER_UP_MAX_SPEED_MULT);
		return new PowerUpSpeed (mult);
	}

	public static PowerUp GetPowerUpAttack() {
		float mult = Random.Range(Constants.POWER_UP_MIN_ATTACK_MULT, Constants.POWER_UP_MAX_ATTACK_MULT);
		return new PowerUpAttack (mult);
	}

	public static PowerUp GetPowerUpNull() {
		return new PowerUpNull ();
	}
}
