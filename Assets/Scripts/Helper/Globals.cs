using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Globals {

	public static Material DefaultMaterial;
	public static Material PowerUpAttackMaterial;
	public static Material PowerUpSpeedMaterial;

    private static GameObject player;
    private static PlayerController playerController;

	public static GameObject GetPlayer() {
        if (player == null)
            player = GameObject.FindGameObjectWithTag(Constants.PLAYER_TAG);

        return player;
	}

	public static PlayerController GetPlayerController() {
        if(playerController == null)
            playerController = GetPlayer().GetComponent<PlayerController>();

        return playerController;
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
