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
		else if (num <= 25) {
			powerUpData = GetPowerUpLife();
		}

		return powerUpData;
	}

	public PowerUpData GetPowerUpSpeed() {
		float mult = Random.Range(Constants.POWER_UP_MIN_SPEED_MULT, Constants.POWER_UP_MAX_SPEED_MULT);
		PowerUpData powerUpData = new PowerUpDataSpeed(mult, Random.Range(4f, 6f));
		return powerUpData;
	}

	public PowerUpData GetPowerUpAttack() {
		float mult = Random.Range(Constants.POWER_UP_MIN_ATTACK_MULT, Constants.POWER_UP_MAX_ATTACK_MULT);
		PowerUpData powerUpData = new PowerUpDataAttack(mult, Random.Range(4f, 6f));
		return powerUpData;
	}

	public PowerUpData GetPowerUpLife() {
		return new PowerUpDataLife();
	}

	public static PowerUpData GetPowerUpNull() {
		PowerUpData powerUpData = new PowerUpDataNull();
		return powerUpData;
	}

	public GameObject InstantiatePowerUp(PowerUpData powerUpData, Vector3 position, Quaternion rotation) {
		if (powerUpData.type == 2) { // power up null
			return null;
		}

		PowerUp powerUp = this.powerUps[powerUpData.type];

        GameObject powerUpObj = Instantiate(powerUp.gameObject, position, rotation) as GameObject;
		powerUpObj.GetComponent<PowerUp>().PowerUpData = powerUpData;

        return powerUpObj;
	}
}
