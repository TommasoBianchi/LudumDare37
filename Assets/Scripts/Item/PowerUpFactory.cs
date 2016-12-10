using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpFactory : MonoBehaviour {

	public List<PowerUp> powerUps;

	private static PowerUpFactory instance;

	public static PowerUpFactory getInstance() {
		return instance;
	}

	void Start() {
		instance = this;
	}

	public PowerUpData GetRandomPowerUp() {
		int num = Random.Range(0, 100);
		PowerUpData powerUpData = GetPowerUpNull();

		if (num <= 10) {
			powerUpData = GetPowerUpAttack();
		}
		else if (num <= 20) {
			powerUpData = GetPowerUpSpeed();
		}

		return powerUpData;
	}

	public PowerUpData GetPowerUpSpeed() {
		float mult = Random.Range(Constants.POWER_UP_MIN_SPEED_MULT, Constants.POWER_UP_MAX_SPEED_MULT);
		PowerUpData powerUpData = new PowerUpData();
		powerUpData.type = 1;
		powerUpData.Duration = 5;
		powerUpData.mult = mult;
		return powerUpData;
	}

	public PowerUpData GetPowerUpAttack() {
		float mult = Random.Range(Constants.POWER_UP_MIN_ATTACK_MULT, Constants.POWER_UP_MAX_ATTACK_MULT);
		PowerUpData powerUpData = new PowerUpData();
		powerUpData.type = 0;
		powerUpData.Duration = 5;
		powerUpData.mult = mult;
		return powerUpData;
	}

	public static PowerUpData GetPowerUpNull() {
		PowerUpData powerUpData = new PowerUpData();
		powerUpData.type = 2;
		powerUpData.Duration = 100;
		return powerUpData;
	}

	public GameObject InstantiatePowerUp(PowerUpData powerUpData, Vector3 position, Quaternion rotation) {
		PowerUp powerUp = this.powerUps[powerUpData.type];

        GameObject powerUpObj = Instantiate(powerUp.gameObject, position, rotation) as GameObject;
		powerUpObj.GetComponent<PowerUp>().PowerUpData = powerUpData;

        return powerUpObj;
	}
}
