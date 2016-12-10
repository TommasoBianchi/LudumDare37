using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAttack : PowerUp {

	private float mult;
	private ParticleEmitter effect;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public PowerUpAttack(float mult) {
		this.mult = mult;
	}

	public void OnStart() {
		GameObject player = Globals.GetPlayer();
		player.GetComponent<SpriteRenderer>().material = Constants.POWER_UP_ATTACK_MATERIAL;
	}

	public void OnFinish() {
		GameObject player = Globals.GetPlayer();
		player.GetComponent<SpriteRenderer>().material = Constants.DEFAULT_MATERIAL;
	}

	public void OnAttack() {
		GameObject player = Globals.GetPlayer();
		Vector3 effectPos = player.transform.position + player.transform.up;
		Instantiate(effect, effectPos, Quaternion.identity);
	}
}
