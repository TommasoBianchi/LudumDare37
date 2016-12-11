using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDataAttack : PowerUpData {

	//private ParticleEmitter effect;

	public PowerUpDataAttack(float mult, float duration) {
		this.type = 0;
		this.mult = mult;
		this.Duration = duration;
	}

	override public void OnStart() {
		Globals.GetPlayer().GetComponent<SpriteRenderer>().material.SetColor("_Color", Constants.COLOR_OVERLAY_BONUS_ATTACK);
		Globals.GetPlayerController().BasePower = mult;
	}

	override public void OnFinish() {
		Globals.GetPlayer().GetComponent<SpriteRenderer>().material.SetColor("_Color", Constants.COLOR_OVERLAY_DEFAULT);
		Globals.GetPlayerController().BasePower = 1f;
	}

	override public void OnAttack() {
		/*GameObject player = Globals.GetPlayer();
		Vector3 effectPos = player.transform.position + player.transform.up;
		Instantiate(effect, effectPos, Quaternion.identity);*/
	}
}
