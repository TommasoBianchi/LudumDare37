using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants {

	public static string PLAYER_TAG = "Player";

	public static float PLAYER_BASE_SPEED = 1.0f;

	public static Material DEFAULT_MATERIAL = Material.Create("Sprites-Default");
	public static Material POWER_UP_ATTACK_MATERIAL = Material.Create("PowerUpAttack");
	public static Material POWER_UP_SPEED_MATERIAL = Material.Create("PowerUpSpeed");

	public static float POWER_UP_MIN_SPEED_MULT = 1.2f;
	public static float POWER_UP_MAX_SPEED_MULT = 2.0f;
	public static float POWER_UP_MIN_ATTACK_MULT = 1.2f;
	public static float POWER_UP_MAX_ATTACK_MULT = 2.0f;

}
