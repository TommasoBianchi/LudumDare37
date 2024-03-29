﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData {

	public int Tier;
    public WeaponType Type;
	public Roll Roll;

	public WeaponData(WeaponType type, int tier, Roll roll) {
		this.Type = type;
		this.Tier = tier;
		this.Roll = roll;
	}

    public override string ToString()
    {
        return "Weapon: " + Type + " " + Roll + " tier " + Tier;
    }
}
