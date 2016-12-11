using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour, Item {

	public PowerUpData PowerUpData;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Collision with PowerUp and layer " + other.gameObject.layer);
        if (other.gameObject.layer == 10) { // player
			Debug.Log("Collision between PowerUp and player");
            OnPickup();
        }
    }
		
	public void OnPickup () {
		Debug.Log("Taken power up with type " + this.PowerUpData.type);
		Globals.GetPlayerController().PowerUpManager.SetPowerUp(this.PowerUpData);
		Destroy(gameObject);
	}

	public void OnStart() {}

	public void OnFinish() {}

	public void OnAttack() {}

	public bool IsNull() {
		return false;
	}
}
