using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WeaponFactory : MonoBehaviour {

	public List<Weapon> weapons;

	private static WeaponFactory instance;

	public static WeaponFactory getInstance() {
		return instance;
	}

	void Start() {
		instance = this;
	}

	public WeaponData GetWeapon(int tier) {
		WeaponType wt = RandomEnumPicker.GetRandomWeaponType();
		Roll roll = RandomEnumPicker.GetRandomRollType();
		Debug.Log("Weapon: " + wt + " " + roll + " tier " + tier);
		WeaponData wd = new WeaponData(wt, tier, roll);
		return wd;
	}

	public GameObject InstantiateShot(WeaponData weaponData, Vector3 position, Quaternion rotation) {

        Weapon weapon = this.weapons.Find(w => w.weaponData.Type == weaponData.Type);

        GameObject weaponObj = Instantiate(weapon.gameObject, position, rotation) as GameObject;
		weaponObj.GetComponent<Weapon>().weaponData = weaponData;
		weaponObj.GetComponent<Bullet>().Range = weapon.baseRange;
		weaponObj.GetComponent<Bullet>().Speed = weapon.baseSpeed;

        return weaponObj;
	}
}
