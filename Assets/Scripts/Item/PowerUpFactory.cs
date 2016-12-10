using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFactory {

	public static PowerUp GetPowerUpSpeed(float mult) {
		return new PowerUpSpeed (mult);
	}

	public static PowerUp GetPowerUpAttack(float mult) {
		return new PowerUpAttack (mult);
	}

	public static PowerUp GetPowerUpNull() {
		return new PowerUpNull ();
	}
}
