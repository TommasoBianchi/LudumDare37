using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory {

	public static Weapon GetWeapon(int tier) {
		WeaponType wt = RandomEnumPicker.GetRandomWeaponType();
		Roll roll = RandomEnumPicker.GetRandomRollType();
		return new Weapon (wt, tier, roll);
	}
}
