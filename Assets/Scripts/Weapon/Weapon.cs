using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public int Tier;
    public WeaponTypes Type;
    public int DmgMod;

    

}

public enum WeaponTypes
{
    Sword,
    Lance,
    Hammer,
    PickAxe
};