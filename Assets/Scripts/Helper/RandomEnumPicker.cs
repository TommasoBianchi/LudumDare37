using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RandomEnumPicker {

	public static WeaponType GetRandomWeaponType() {
		Array values = Enum.GetValues(typeof(WeaponType));
		System.Random random = new System.Random();
		return (WeaponType)values.GetValue(random.Next(values.Length));
	}

	public static ResourceType GetRandomResourceType() {
		Array values = Enum.GetValues(typeof(ResourceType));
		System.Random random = new System.Random();
		return (ResourceType)values.GetValue(random.Next(values.Length));
	}

	public static Roll GetRandomRollType() {
		Array values = Enum.GetValues(typeof(Roll));
		System.Random random = new System.Random();
		return (Roll)values.GetValue(random.Next(values.Length));
	}
}
