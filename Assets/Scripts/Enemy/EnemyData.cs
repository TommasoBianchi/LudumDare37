using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData {

	public int type; // index of EnemyFactory.enemies.
	public List<WeaponType> Weaknesses = new List<WeaponType>();
	public List<WeaponType> Resistences = new List<WeaponType>();
	public float Life;

	public PowerUpData PowerUpData = PowerUpFactory.GetPowerUpNull();
	public float Scale;
	public Color ColorOverlay;

}
