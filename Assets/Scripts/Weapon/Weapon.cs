using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public WeaponData weaponData;

    public float baseDamage = 1f;
    public float baseRange = 3f;
    public float baseFrequency = 3f;
    public float baseSpeed = 1f;

    //Costruttore per quando si crea l'arma
	public Weapon(WeaponData wd) {
		this.weaponData = wd;
	}

    public override string ToString()
    {
        return weaponData.ToString();
    }
}
