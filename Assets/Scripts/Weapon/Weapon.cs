using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public WeaponData weaponData;

    //Costruttore per quando si crea l'arma
	public Weapon(WeaponData wd) {
		this.weaponData = wd;
	}

    public override string ToString()
    {
        return weaponData.ToString();
    }
}
