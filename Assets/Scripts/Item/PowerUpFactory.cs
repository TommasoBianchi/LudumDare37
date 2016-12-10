using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFactory {

	public static PowerUp GetPowerUpSpeed(float mult) {

	}

	public static PowerUp GetPowerUpAttack(float mult) {

	}

	public static PowerUp GetPowerUpNull() {
		return new PowerUpNull ();
	}
}
