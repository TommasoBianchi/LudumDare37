using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private static Text roomNumberUI;

    private static int currentLevel = 0;
    public static int CurrentLevel
    {
        get { return Globals.currentLevel; }
        set 
        {
            if (roomNumberUI == null)
                roomNumberUI = GameObject.Find("RoomNumber").GetComponentInChildren<Text>();
            if (roomNumberUI == null)
                throw new UnityException("Fix me!");

            roomNumberUI.text = value.ToString();

            Globals.currentLevel = value; 
        }
    }

}
