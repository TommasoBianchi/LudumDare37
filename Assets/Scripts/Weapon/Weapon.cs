using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int Tier;
    public WeaponType Type;
	public Roll Roll; //SOLO PER IL CRAFT


    //Costruttore per quando si crea l'arma
	public Weapon(WeaponType type, int tier, Roll roll) {
		this.Type = type;
		this.Tier = tier;
		this.Roll = roll;
	}

    //Costruttore per la ricetta
    public Weapon(WeaponType type, int tier) {
        this.Type = type;
        this.Tier = tier;
    }
}
