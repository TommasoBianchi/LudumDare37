using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory : MonoBehaviour {

	public List<Weapon> weapons;

	private static WeaponFactory instance;

	public static WeaponFactory getInstance() {
		return instance;
	}

	void Start() {
		instance = this;
	}

	public static WeaponData GetWeapon(int tier) {
		WeaponType wt = RandomEnumPicker.GetRandomWeaponType();
		Roll roll = RandomEnumPicker.GetRandomRollType();
		WeaponData wd = new WeaponData(wt, tier, roll);
		return wd;
	}

	public GameObject InstantiateShot(WeaponData weaponData, Vector3 position, Quaternion rotation) {
		Array values = Enum.GetValues(typeof(WeaponType));
		int pos = 0;
		for (int i = 0; i < values.Length; i++)
		{
			if ((WeaponType)values.GetValue(i) == weaponData.Type) {
				pos = i;
				break;
			}
		}

		Weapon weapon = this.weapons[pos];

        GameObject weaponObj = Instantiate(weapon.gameObject, position, rotation) as GameObject;
		weaponObj.GetComponent<Weapon>().weaponData = weaponData;

        return weaponObj;
	}
}
