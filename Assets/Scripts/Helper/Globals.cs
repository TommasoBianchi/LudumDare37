using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals {

	public static Material DefaultMaterial;
	public static Material PowerUpAttackMaterial;
	public static Material PowerUpSpeedMaterial;

	public static GameObject GetPlayer() {
		return GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG);
	}

	public static PlayerController GetPlayerController() {
		return GetPlayer().GetComponent<PlayerController>();
	}

}
