using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnumPicker {

	public static WeaponType GetRandomWeaponType() {
		Array values = Enum.GetValues(typeof(WeaponType));
		Random random = new Random();
		return (WeaponType)values.GetValue(random.Next(values.Length));
	}

	public static ResourceType GetRandomResourceType() {
		Array values = Enum.GetValues(typeof(ResourceType));
		Random random = new Random();
		return (ResourceType)values.GetValue(random.Next(values.Length));
	}

	public static Roll GetRandomRollType() {
		Array values = Enum.GetValues(typeof(Roll));
		Random random = new Random();
		return (Roll)values.GetValue(random.Next(values.Length));
	}
}
