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
        if (other.gameObject.layer == 10) { // player
            OnPickup();
        }
    }
		
	public void OnPickup () {
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
