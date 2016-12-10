using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour {
	private static WeaponFactory instance;

	public static WeaponFactory getInstance() {
		return instance;
	}

	void Start() {
		instance = this;
	}

	public static Weapon GetWeapon(int tier) {
		WeaponType wt = RandomEnumPicker.GetRandomWeaponType();
		Roll roll = RandomEnumPicker.GetRandomRollType();
		return new Weapon (wt, tier, roll);
	}
}
