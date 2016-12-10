using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals {

	public static GameObject GetPlayer() {
		return GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG);
	}

	public static PlayerController GetPlayerController() {
		return GetPlayer().GetComponent<PlayerController>();
	}
}
